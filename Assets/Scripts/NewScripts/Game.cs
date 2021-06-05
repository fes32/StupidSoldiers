using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private List<DestructibleObject> _barriers;
    [SerializeField] private List<string> _praises;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private LosePanel _gameOverPanel;
    [SerializeField] private GameObject _previewPanel;
    [SerializeField] private FinishZone _finishZone;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private UI _uiPanel;

    private bool _gameStarted = false;
    
    public event UnityAction GameStarted;

    private void OnEnable()
    {
        _currentWeapon._Collided += GameOver;
        _finishZone.GameEnded += OnFinisGame;
        _winPanel.ContinueButtonClick += OnContinueButtonClick;
        _gameOverPanel.RestartButtonClick += OnRestartButtonClick;

        foreach (var barrier in _barriers)
        {
            barrier.Destroyed += OnBarrierDestroyed;
        }

        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _currentWeapon._Collided -= GameOver;
        _finishZone.GameEnded -= OnFinisGame;
        _winPanel.ContinueButtonClick -= OnContinueButtonClick;
        _gameOverPanel.RestartButtonClick -= OnRestartButtonClick;

        foreach (var barrier in _barriers)
        {
            barrier.Destroyed -= OnBarrierDestroyed;
        }
    }

    private void Update()
    {
        if (_gameStarted == false && Input.touchCount > 0 | Input.GetKey(KeyCode.Space))
            StartGame();
        
    }

    private void StartGame()
    {
        _gameStarted = true;
        GameStarted?.Invoke();
        _previewPanel.SetActive(false);
        Time.timeScale = 1;
        _currentWeapon.gameObject.SetActive(true);
        _uiPanel.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        _gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnFinisGame()
    {
        Time.timeScale = 0;
        _winPanel.gameObject.SetActive(true);
    }

    private void OnContinueButtonClick()
    {
        Restart();
    }

    private void OnRestartButtonClick()
    {
        Restart();
    }

    private void OnBarrierDestroyed(DestructibleObject barrier, int reward)
    {
        string praise= _praises[Random.Range(0, _praises.Count)];
        barrier.Destroyed -= OnBarrierDestroyed;

        _uiPanel.AddMoney(reward);
        _uiPanel.ShowPraise(praise);
    }
 
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}