using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region Exposed

    [SerializeField]
    private GameObject _bloodCell;
    [SerializeField]
    private GameObject _slime;
    [SerializeField]
    private GameObject _bigSpike;

    [SerializeField]
    private float _spawnRange;

    public int _waveNumber;
    public int _slimenb;
    public int _bloodnb;
    public int _spikenb;

    private int _enemyCount;

    #endregion


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnManagerLife(_slimenb, _bloodnb, _spikenb);
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 _spawnPosition;
        float _xPos = Random.Range(-_spawnRange, _spawnRange);
        float _zPos = Random.Range(-_spawnRange, _spawnRange);
        _spawnPosition = new Vector3(_xPos, 0f, _zPos);

        return _spawnPosition;
    }

    public void SpawnBlood(int _ennemyToSpawn)
    {
        for (int i = 0; i < _ennemyToSpawn; i++)
        {
            Instantiate(_bloodCell, GenerateSpawnPosition(), _bloodCell.transform.rotation);
        }
    }

    public void SpawnBSlime(int _ennemyToSpawn)
    {
        for (int i = 0; i < _ennemyToSpawn; i++)
        {
            Instantiate(_slime, GenerateSpawnPosition(), _bloodCell.transform.rotation);
        }
    }
    public void SpawnSpike(int _ennemyToSpawn)
    {
        for (int i = 0; i < _ennemyToSpawn; i++)
        {
            Instantiate(_bigSpike, GenerateSpawnPosition(), _bloodCell.transform.rotation);
        }
    }

    public void SpawnWave(int _slime, int _blood, int _spike)
    {
        SpawnBSlime(_slime);
        SpawnBlood(_blood);
        SpawnSpike(_spike);
    }

    public void SpawnManagerLife(int _slime, int _blood, int _spike)
    {
        if (_waveNumber == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (_enemyCount == 0)
            {
                SpawnWave(_slime, _blood, _spike);
                _waveNumber--;
            }
        }
    }
}
