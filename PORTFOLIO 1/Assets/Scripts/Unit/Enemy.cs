using UnityEngine;

public class Enemy : Unit
{
    #region Exposed

    public GameObject _bulletPrefab;
    public Transform _cannonTransform;
    public GameObject _explosionGoo;
    public bool _ranged;
    public float _attackRate;
    public float _bulletSpeed;
    public int _currentHp;
    float m_nextAttackTime = 0f;

    #endregion


    #region Propreties

    protected Transform _playerTransform;

    #endregion

    void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (_ranged)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            EnemyHit();
        }
    }

    private void FixedUpdate()
    {
        RigidBodyApply(_mouvementVector.normalized);
        RigidBodyRotation(_rotationVector.normalized);
    }

    protected override void Move()
    {
        _mouvementVector = _playerTransform.position - _transform.position;
        _mouvementVector.y = 0f;

        Vector3 _lookDirection = Vector3.RotateTowards(transform.forward, _mouvementVector, _turnSpeed * Time.deltaTime, 0f);
        _rotationVector = Quaternion.LookRotation(_lookDirection);
        _rotationVector.x = 0f;

    }

    private void Shoot()
    {
        if (Time.time >= m_nextAttackTime)
        {
            GameObject _clone = Instantiate(_bulletPrefab, _cannonTransform.position, transform.rotation);
            EnemyBullet _enemyBullet = _clone.GetComponent<EnemyBullet>();
            _enemyBullet.EnemyBulletSpeed(_bulletSpeed);
            m_nextAttackTime = Time.time + 1f / _attackRate;
        }
    }

    private void EnemyHit()
    {
        if (_currentHp == 0)
        {
            Instantiate(_explosionGoo, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            _currentHp--;
            Instantiate(_explosionGoo, transform.position, Quaternion.identity);
        }
    }

    #region Privates



    #endregion


}
