using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Exposed

    public List<GameObject> m_targets;

    public float m_spawnRate = 1f;

    public TextMeshProUGUI m_scoreText;

    public TextMeshProUGUI m_gameOverText;

    public Button m_restartButton;

    public bool m_isGameActive;

    public GameObject m_titleScreen;



    #endregion
    void Start()
    {
        


        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnTargets()
    {
        while (m_isGameActive)
        {
            yield return new WaitForSeconds(m_spawnRate);
            int Index = Random.Range(0, m_targets.Count);
            Instantiate(m_targets[Index]);
            //UpdateScore(5);
        }
    }

    public void UpdateScore(int _scoreToAdd)
    {
        _score += _scoreToAdd;
        m_scoreText.text = "Score :" + _score;

    }

    public void GameOver()
    {
        m_gameOverText.gameObject.SetActive(true);
        m_restartButton.gameObject.SetActive(true);
        m_isGameActive = false;
    }

    public void StartGame(int _difficulty)
    {
        m_isGameActive = true;
        StartCoroutine(SpawnTargets());
        _score = 0;
        UpdateScore(0);
        m_titleScreen.SetActive(false);
        m_spawnRate /= _difficulty;
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #region Privates

    private int _score;

    #endregion
}
