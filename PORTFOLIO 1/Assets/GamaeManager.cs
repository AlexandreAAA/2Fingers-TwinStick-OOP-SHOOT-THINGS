using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GamaeManager : MonoBehaviour
{
    public static GamaeManager Instance;
    public CanvasGroup _loadingScreen;
    public Animator _loadingAnim;
    public GameObject _titleScreen;
    public GameObject _gameOverScreen;
    public GameObject _winscreen;
    public GameObject _healthBar;
    public float _loadingTime;
    public bool facdout;
    public bool _gameOver;
    public bool _Playing;

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //StartCoroutine(FadeOutLoad(0));
        _gameOver = false;
        _loadingAnim.SetTrigger("In");
    }

    private void Update()
    {
        
    }

    private IEnumerator FadeInLoad(float _time)
    {
        while (_loadingScreen.alpha < 1)
        {
            _loadingScreen.alpha = Mathf.Lerp(0, 1, _time / _loadingTime);
            _time += Time.deltaTime;
            if (_loadingScreen.alpha > 0.98)
            {
                _loadingScreen.alpha = 1f;
            }
            yield return null;

        }

    }

    private IEnumerator FadeOutLoad(float _time)
    {
        while(_loadingScreen.alpha > 0)
        {
            _loadingScreen.alpha = Mathf.Lerp(1, 0, _time / _loadingTime);
            _time += Time.deltaTime;

            if (_loadingScreen.alpha < 0.2f)
            {
                _loadingScreen.alpha = 0f;
            }
            yield return null;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        //StartCoroutine(FadeInLoad(0));
        _loadingAnim.SetTrigger("Out");
        _titleScreen.SetActive(false);
    }

    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(FadeInLoad(0));
        _loadingAnim.SetTrigger("Out");
        _gameOver = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //StartCoroutine(FadeInLoad(0));
        _loadingAnim.SetTrigger("Out");
        _gameOver = false;
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        _healthBar.SetActive(false);
        _gameOver = true;
        
    }

    public void YouWin()
    {
        _winscreen.SetActive(true);
        _healthBar.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#else

        Application.Quit();

#endif
    }

    private float _time;
}
