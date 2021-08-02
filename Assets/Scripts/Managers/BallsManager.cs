using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{

    #region Singleton
    private static BallsManager _instance;
    public static BallsManager Instance => _instance;
    
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

    #region Fields
    [SerializeField]
    private Ball _ballPrefab;
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;
    private const int MAXBALLCOUNT = 10;
    #endregion

    #region Properties
    public float InitialBallSpeed => 250;
    public List<Ball> Balls { get; set; }
    #endregion
    private void Start()
    {
        InitBall();
    }
    public void SpawnBalls(Vector3 position, int count, bool isLightningBall)
    {
        for (var i = 0; i < count; i++)
        {
            if (Balls.Count >= MAXBALLCOUNT) return;
            Ball spawnedBall = Instantiate(_ballPrefab, position, Quaternion.identity) as Ball;
            if (isLightningBall)
            {
                spawnedBall.StartLightningBall();
            }
            Rigidbody2D rigidbody = spawnedBall.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = false;
            var xSpeed = i % 2 == 0 ? InitialBallSpeed : -InitialBallSpeed;
            rigidbody.AddForce(new Vector2(xSpeed/2, InitialBallSpeed/2));
            Balls.Add(spawnedBall);
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, paddlePosition.z);
            _initialBall.transform.position = ballPosition;
            if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsOnMenu)
            {
                _initialBallRb.isKinematic = false;
                _initialBallRb.AddForce(new Vector2(0, InitialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    public void ResetBalls()
    {
        foreach(var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }
        InitBall();
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x,paddlePosition.y + .27f,paddlePosition.z);
        _initialBall = Instantiate(_ballPrefab, startingPosition, Quaternion.identity);
        _initialBallRb = _initialBall.GetComponent<Rigidbody2D>();
        this.Balls = new List<Ball> { _initialBall };
    }
}
