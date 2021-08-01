using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text LivesText;
    public Text ScoreText;
    public int Score { get; set; }
    private void Start()
    {
        Brick.OnBrickDestruction += OnBrickDestruction;
        GameManager.Instance.OnLifeLost += OnLifeLost;
        UpdateScoreText(0);
        OnLifeLost(GameManager.Instance.AvailableLives);
    }

    private void OnLifeLost(int remainingLives)
    {
        LivesText.text = $"LIVES: {remainingLives}";
    }

    private void OnBrickDestruction(Brick obj)
    {
        UpdateScoreText(10*obj.Dificulty);
    }

    private void UpdateScoreText(int increment)
    {
        Score += increment;
        var scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = $"SCORE:{Environment.NewLine}{scoreString}";
    }
    private void OnDisable()
    {
        GameManager.Instance.OnLifeLost -= OnLifeLost;
        Brick.OnBrickDestruction -= OnBrickDestruction;
    }
}
