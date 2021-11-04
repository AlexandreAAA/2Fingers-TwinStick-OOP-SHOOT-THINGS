using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region Exposed

    public float m_minSpeed;
    public float m_maxSpeed;
    public float m_maxTorque;
    public float m_xRange;
    public float m_ySpawnPos;

    public int m_valueInPoint;

    public ParticleSystem m_explosiionParticles;
    #endregion
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();

        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (_gameManager.m_isGameActive)
        {

            Instantiate(m_explosiionParticles, transform.position, m_explosiionParticles.transform.rotation);
            Destroy(gameObject);
            _gameManager.UpdateScore(m_valueInPoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {

            _gameManager.GameOver();
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(m_minSpeed, m_maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-m_maxTorque, m_maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-m_xRange, m_xRange), m_ySpawnPos);
    }
    #region Privates

    private Rigidbody _targetRb;

    private GameManager _gameManager;

    #endregion
}
