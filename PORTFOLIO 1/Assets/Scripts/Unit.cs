using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _turnSpeed;

    #endregion


    #region Privates

    protected Transform _transform;
    protected Rigidbody _rigidbody;
    protected Vector3 _mouvementVector;
    protected Quaternion _rotationVector;

    #endregion


    #region Unity API

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    protected virtual void Move()
    {

    }

    protected void RigidBodyApply(Vector3 _direction)
    {
        _rigidbody.velocity = new Vector3(_direction.x * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y, _direction.z * _moveSpeed * Time.fixedDeltaTime);
        //_rigidbody.AddForce(new Vector3(_direction.x * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y, _direction.z * _moveSpeed * Time.fixedDeltaTime));
        //_rigidbody.MovePosition(_transform.position + new Vector3(_direction.x * _moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y, _direction.z * _moveSpeed * Time.fixedDeltaTime));
    }

    protected void RigidBodyRotation(Quaternion _rotation)
    {
        _rigidbody.MoveRotation(_rotation);
    }


    #endregion


}
