using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnActivate : MonoBehaviour
{
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gamer"))
        {
            //_spawnManager.SpawnOn = true;
            _spawnManager.enabled = true;
        }
    }
}
