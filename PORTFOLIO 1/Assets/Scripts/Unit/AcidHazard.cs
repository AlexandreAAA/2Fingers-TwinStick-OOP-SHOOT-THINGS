using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidHazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gamer"))
        {
            Player _player = other.GetComponent<Player>();
            if (_player != null)
            {
                _player.PlayerHit();
            }
        }
    }
}
