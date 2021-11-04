using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    #region Exposed

    public int _difficulty;

    #endregion
    void Start()
    {
        _difficultyButton = GetComponent<Button>();
        _difficultyButton.onClick.AddListener(SetDifficulty);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDifficulty()
    {
        Debug.Log(gameObject.name + "was Clicked");
        _gameManager.StartGame(_difficulty);
    }

    #region Privates

    private Button _difficultyButton;

    private GameManager _gameManager;

    #endregion
}
