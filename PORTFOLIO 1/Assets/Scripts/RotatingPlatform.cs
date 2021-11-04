using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    #region Exposed

    [SerializeField]
    private Vector3 _rotatingVector;
    public float _turnSpeed;

    #endregion


    #region privates

    private Rigidbody _rb;

    #endregion


    #region Unity API

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    

    #endregion

    #region Method

    private void Rotate()
    {
        _rotatingVector.y += Time.deltaTime * _turnSpeed;
        _rb.MoveRotation(Quaternion.Euler(_rotatingVector));
    }


    
    #endregion
}
