using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        _bulletRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SHoots();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void SHoots()
    {
        Vector3 _velocity = transform.forward * _bulletSpeed * Time.fixedDeltaTime;
        Vector3 newPos = transform.position + _velocity;
        _bulletRb.MovePosition(newPos);
    }

    public void EnemyBulletSpeed(float _speed)
    {
        _bulletSpeed = _speed;
    }

    private Rigidbody _bulletRb;
}
