using System.Collections;
using System.Collections.Generic;
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
    #endregion

    #region Properties
    public float InitialBallSpeed => 250;
    public List<Ball> Balls { get; set; }
    #endregion
    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, paddlePosition.z);
            _initialBall.transform.position = ballPosition;
            if (Input.GetMouseButtonDown(0))
            {
                _initialBallRb.isKinematic = false;
                _initialBallRb.AddForce(new Vector2(0, InitialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
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
