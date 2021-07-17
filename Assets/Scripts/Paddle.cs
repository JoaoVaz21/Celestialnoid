using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton
    private static Paddle _instance;
    public static Paddle Instance => _instance;
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

    private Camera _mainCamera;
    private float _paddleInitialY;
    private SpriteRenderer _spriteRenderer;

    private const float DefaultPaddleWidthInPixels = 200;
    private const float DefaultLeftClamp = 135;
    private const float DefaultRightClamp = 410;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _paddleInitialY = this.transform.position.y;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMovement();
    }
    private void PaddleMovement()
    {
        float paddleShift = (DefaultPaddleWidthInPixels - ((DefaultPaddleWidthInPixels /2) * this._spriteRenderer.size.x))/ 2;
        float leftClamp = DefaultLeftClamp - paddleShift ;
        float rightClamp = DefaultRightClamp + paddleShift;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = _mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePositionWorldX, _paddleInitialY, 0);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;
            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.InitialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.InitialBallSpeed));

            }
        }
    }
}
