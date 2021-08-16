using System;
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
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private float _input;
    private const float Speed = 5f;
    private const float DefaultPaddleWidthInPixels = 200;
    private const float DefaultLeftClamp = 135;
    private const float DefaultRightClamp = 410;
    private const float ExtendShrinkDuration = 3;
    private const float DefaultPaddleWidth = 1;
    private const float DefaultPaddleHeight = 0.28f;

    public bool PaddleIsTransforming { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _paddleInitialY = this.transform.position.y;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _input = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.right * _input * Speed;
    }

    public void StartWidthAnimation(float newWidth, bool isReseting = false)
    {
        if (!PaddleIsTransforming || isReseting)
        {
            StartCoroutine(AnimatePaddleWidth(newWidth, !isReseting));
        }
    }

    private IEnumerator AnimatePaddleWidth(float newWidth, bool shouldReset)
    {
            PaddleIsTransforming = true;
            if(shouldReset) StartCoroutine(ResetPaddleWidthAfterTime(ExtendShrinkDuration));
            if (newWidth > _spriteRenderer.size.x)
            {
                float currentWidth = _spriteRenderer.size.x;
                while (currentWidth < newWidth)
                {
                    currentWidth += Time.deltaTime * 2;
                    _spriteRenderer.size = new Vector2(currentWidth, DefaultPaddleHeight);
                    _boxCollider2D.size = new Vector2(currentWidth, DefaultPaddleHeight);
                    yield return null;
                }
            }
            else
            {
                float currentWidth = _spriteRenderer.size.x;
                while (currentWidth > newWidth)
                {
                    currentWidth -= Time.deltaTime * 2;
                    _spriteRenderer.size = new Vector2(currentWidth, DefaultPaddleHeight);
                    _boxCollider2D.size = new Vector2(currentWidth, DefaultPaddleHeight);
                    yield return null;
                }
            }
        _boxCollider2D.size = new Vector2(newWidth, DefaultPaddleHeight);
    }

    private IEnumerator ResetPaddleWidthAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartWidthAnimation(DefaultPaddleWidth, true);
        PaddleIsTransforming = false;

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
