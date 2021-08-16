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
            MaxBrickHitPoints = 1;
        }
    }
    #endregion

    public bool IsGameStarted { get; set; }
    public int Lives { get; set; }
    public int MaxBrickHitPoints { get; set; }
    public int AvailableLives = 3;
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;
    public event Action<int> OnLifeChanged;
    private int _levelCount = 1;

    private void Start()
    {
        this.Lives = AvailableLives;
        Screen.SetResolution(540, 960, false);
        Ball.OnBallDeath += OnBallDeath;
        BricksManager.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        GameManager.Instance.IsGameStarted = false;
        Time.timeScale = 0;
        CollectablesManager.Instance.DestroyCollectables();
        BallsManager.Instance.ResetBalls();
        UpdateDificulty();
        BricksManager.Instance.LoadNextLevel();
        VictoryScreen.SetActive(true);

    }
  
    private void UpdateDificulty()
    {
        _levelCount++;
       
                if (_levelCount % 2 == 0 && MaxBrickHitPoints<5)
                {
                    MaxBrickHitPoints++;
                }
                if(_levelCount%3==0 && BricksManager.Instance.CurrentMaxRowNumber < 12)
                {
                    BricksManager.Instance.CurrentMaxRowNumber++;
                }
        if (_levelCount % 4 == 0)
        {
            BallsManager.Instance.InitialBallSpeed += 30;
        }
    }
    public void NextLevel()
    {
        BricksManager.Instance.GenerateBricks();
        VictoryScreen.SetActive(false);
        Time.timeScale = 1;

    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void AddLife()
    {
        Lives++;
        OnLifeChanged?.Invoke(this.Lives);
    }
    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;
            if (this.Lives < 1)
            {
                GameOverScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                OnLifeChanged?.Invoke(this.Lives);
                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
            }
        }
    }
    
    private void OnDisable()
    {
        BricksManager.OnLevelCompleted -= OnLevelCompleted;
        Ball.OnBallDeath -= OnBallDeath;
    }
}
