using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    // Start is called before the first frame update
   private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public bool IsGameStarted { get; set; }
    public bool IsOnMenu { get; set; }
    public int Lives { get; set; }
    public int AvailableLives = 3;
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;
    public event Action<int> OnLifeLost;

    private void Start()
    {
        this.Lives = AvailableLives;
        Screen.SetResolution(540, 960, false);
        Ball.OnBallDeath += OnBallDeath;
        BricksManager.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        BallsManager.Instance.ResetBalls();
        GameManager.Instance.IsGameStarted = false;
        GameManager.Instance.IsOnMenu = true;
        BricksManager.Instance.LoadNextLevel();
        VictoryScreen.SetActive(true);

    }
    public void NextLevel()
    {
        BricksManager.Instance.GenerateBricks();
        VictoryScreen.SetActive(false);
        GameManager.Instance.IsOnMenu = false;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;
            if (this.Lives < 1)
            {
                GameOverScreen.SetActive(true);
                GameManager.Instance.IsOnMenu = true;
            }
            else
            {
                OnLifeLost?.Invoke(this.Lives);
                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
            }
        }
    }
    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }
}
