using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region exposed

    public GameObject _explosion;

    #endregion
    void Start()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        GoForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    #region Main Method

    public void BulletSpeed(float _speed)
    {
        _bulletSpeed = _speed;
    }

    protected void GoForward()
    {
        Vector3 _velocity = transform.forward * _bulletSpeed * Time.fixedDeltaTime;
        Vector3 newPos = transform.position + _velocity;
        _bulletRigidbody.MovePosition(newPos);
    }

    #endregion

    #region Privates

    protected Rigidbody _bulletRigidbody;
    protected float _bulletSpeed;

    #endregion
}
