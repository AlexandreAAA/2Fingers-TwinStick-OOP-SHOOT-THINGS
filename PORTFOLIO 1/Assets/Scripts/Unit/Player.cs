using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Player : Unit
{
    #region Exposed

    //PARAMETERS
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _gravityMultplifier;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private float _invincibleTime;
    [SerializeField]
    private float _hitForce;

    public float _attackRate = 2f;

    public GameObject _bulletPrefab;
    public GameObject _explosion;
    public Transform _cannon;

    public IntVariable _playermaxHp;
    public IntVariable _playerCurrentHp;


    #endregion


    #region Privates

    //INPUTS
    public bool _jump;
    bool m_jump;
    bool m_shoot;
    float _moveX;
    float _moveY;
    float _hRot;
    float _vRot;

    private float m_nextAttackTime = 0f;
    private Rigidbody _bulletRigidbody;

    #endregion


    #region Unity API

    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerCurrentHp._value = _playermaxHp._value;
    }


    void Update()
    {
        Move();

        m_jump = _jump && isGrounded;

        if (Time.time >= m_nextAttackTime)
        {
            //if (Input.GetButtonDown("Fire1"))
            if (m_shoot)
            {
                Shoot();
                m_nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
    }

    private void FixedUpdate()
    {
        RigidBodyApply(_mouvementVector);
        RigidBodyRotation(_rotationVector.normalized);
        Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("EnemyBullet"))
        {
            if (_isInvincible)
            {
                return;
            }
            else
            {
                PlayerHit();
                Rigidbody _ennemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 _awayFromPlayer = collision.gameObject.transform.position - transform.position;
                _awayFromPlayer.y = 0f;
                //_ennemyRb.AddForce(_awayFromPlayer * _hitForce, ForceMode.Impulse);
                _rigidbody.AddForce(_awayFromPlayer * -1 * _hitForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            isGrounded = true;
            _transform.parent = other.transform;
            _rigidbody.velocity += other.GetComponent<Rigidbody>().velocity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            isGrounded = false;
            _transform.parent = null;

        }
    }

    #endregion


    #region Main Method

    private void OnMove(InputValue _movementValue)
    {
        Vector2 _moveVector = _movementValue.Get<Vector2>();

        _moveX = _moveVector.x;
        _moveY = _moveVector.y;

    }

    private void OnJump(InputValue _value)
    {
        if (_value.isPressed)
        {
            m_jump = true;
        }
        else
        {
            m_jump = false;
        }
    }

    private void OnFire(InputValue _value)
    {
        m_shoot = _value.isPressed;
    }

    private void OnRotate(InputValue _value)
    {
        Vector2 _rotVector = _value.Get<Vector2>();

        _hRot = _rotVector.x;
        _vRot = _rotVector.y;
    }

    protected override void Move()
    {
        //float _horizontal = Input.GetAxis("Horizontal");
        //float _vertical = Input.GetAxis("Vertical");
        //float _hRot = Input.GetAxis("Horizontal2");
        //float _vRot = Input.GetAxis("Vertical2");

        //_mouvementVector = new Vector3(_horizontal, 0f, _vertical);
        _mouvementVector = new Vector3(_moveX, 0f, _moveY);
        _mouvementVector.Normalize();

        Vector3 _rotation = new Vector3(_hRot, 0f, _vRot);
        _rotation.Normalize();


        if (_rotation.sqrMagnitude > 0.1f)
        {
            Vector3 _lookDirection = Vector3.RotateTowards(_transform.forward, _rotation, _turnSpeed * Time.deltaTime, 0f);
            _rotationVector = Quaternion.LookRotation(_lookDirection, Vector3.up);
        }
    }

    private void Jump()
    {
        if (m_jump && isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            float _jumpForce = Mathf.Sqrt(_jumpHeight * -2 * Physics.gravity.y);
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0) * Time.fixedDeltaTime * 50, ForceMode.Impulse);
        }

        if (_rigidbody.velocity.y < -0.01)
        {
            _rigidbody.AddForce(Physics.gravity * _gravityMultplifier, ForceMode.Acceleration);
        }

        PlayerFall();
    }

    private void Shoot()
    {

        GameObject _clone = Instantiate(_bulletPrefab, _cannon.transform.position, _cannon.transform.rotation);
        Bullet _bullet = _clone.GetComponent<Bullet>();
        _bullet.BulletSpeed(_bulletSpeed);

    }

    public void PlayerHit()
    {
        if (_playerCurrentHp._value == 0 || _transform.position.y < -10f)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            
        }
        else if (!_isInvincible)
        {
            _anim.SetBool("Hit", true);
            _isInvincible = true;
            _playerCurrentHp._value--;
            StartCoroutine(Invincible());
        }
    }

    private void PlayerFall()
    {
        if (_transform.position.y < -10f)
        {
            _playerCurrentHp._value = 0;
            PlayerHit();
        }
    }

    private IEnumerator Invincible()
    {
        yield return new WaitForSeconds(_invincibleTime);
        _anim.SetBool("Hit", false);
        _isInvincible = false;
    }

    #endregion


    #region Privates

    private bool _isInvincible = false;
    private Animator _anim;

    #endregion

}
