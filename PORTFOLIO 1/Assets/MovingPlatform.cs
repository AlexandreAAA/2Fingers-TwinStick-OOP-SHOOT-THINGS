using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{

    #region Exposed

    
    public float _platformSpeed;
    public bool m_isMoving = false;
    public Transform[] _pos;
    Vector3 _targetPos;

    #endregion
    void Start()
    {
        _platformRb = GetComponent<Rigidbody>();

        StartCoroutine(Moving(_pos[0].position, _platformSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if (_platformRb.position == _pos[0].position)
        {
            StartCoroutine(Moving(_pos[1].position, _platformSpeed));
        }

        if (_platformRb.position == _pos[1].position)
        {
            StartCoroutine(Moving(_pos[0].position, _platformSpeed));
        }
    }

    private void FixedUpdate()
    {
        _platformRb.MovePosition(_targetPos);
    }

    private IEnumerator Moving(Vector3 _target, float _speed)
    {
        Vector3 _startPos = transform.position;
        float _time = 0f;

        while(_platformRb.position != _target)
        {
            _targetPos = Vector3.Lerp(_startPos, _target, (_time * _speed) / Vector3.Distance(_startPos, _target));
            _time += Time.deltaTime;
            yield return null;
        }
    }

    #region Privates

    private Rigidbody _platformRb;

    #endregion
}
