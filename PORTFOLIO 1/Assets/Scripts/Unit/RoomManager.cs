using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private Animator _doorAnim;
    [SerializeField]
    private GameObject _spawnManager;

    #endregion

    #region Unity Api

    void Start()
    {
        _ennemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    
    void Update()
    {
        
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gamer"))
        {
            DoorOpen();
        }
    }
    #endregion


    #region Method

    private void DoorOpen()
    {
        _ennemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (_ennemyCount == 0 && _spawnManager == null)
        {
            _doorAnim.SetTrigger("Open");
        }
    }

    #endregion



    #region Privates

    [SerializeField]
    private int _ennemyCount;

    #endregion
}
