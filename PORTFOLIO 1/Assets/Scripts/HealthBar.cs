using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    #region Exposed

    public IntVariable _playerMaxHp;
    public IntVariable _playerCurrentHp;

    #endregion

    void Start()
    {
        _healthBar = GetComponent<Slider>();
    }

    
    void Update()
    {
        float _maxHp = _playerMaxHp._value;
        float _hp = _playerCurrentHp._value;
        _healthBar.value = Mathf.Clamp01( _hp / _maxHp);
    }

    #region Privates

    private Slider _healthBar;

    #endregion
}
