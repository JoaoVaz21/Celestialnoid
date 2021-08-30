using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private const float LightningBallDuration = 4.0f;
    private Rigidbody2D _rigidbody2D;
    private const float SmoothingFactor = 1.0f;
    public ParticleSystem LightningBallEffect;
    public bool IsLightningBall;
    public float currentYSpeed;
    public static event Action<Ball> OnBallDeath;
    public static event Action<Ball> OnLightningBallEnable;
    public static event Action<Ball> OnLightningBallDisable;


    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }
    private void Awake()
    {
        currentYSpeed = BallsManager.Instance.InitialBallSpeed;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();  
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void MultiplySpeed(float factor)
    {
        currentYSpeed = BallsManager.Instance.InitialBallSpeed* factor;
        _rigidbody2D.AddForce ( new Vector2(_rigidbody2D.velocity.x * factor, currentYSpeed));
    }
    public void StartLightningBall()
    {
        if (!IsLightningBall)
        {
            IsLightningBall = true;
            _spriteRenderer.enabled = false;
            LightningBallEffect.gameObject.SetActive(true);
            StartCoroutine(StopLightningBallAfterTime(LightningBallDuration));
            OnLightningBallEnable?.Invoke(this);
        }
    }

    private IEnumerator StopLightningBallAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StopLightningBall();
    }

    private void StopLightningBall()
    {
        if (IsLightningBall)
        {
            IsLightningBall = false;
            _spriteRenderer.enabled = true;
            LightningBallEffect.gameObject.SetActive(false);
            OnLightningBallDisable?.Invoke(this);

        }
    }
    private void Update()
    {
        var cvel = _rigidbody2D.velocity;
        var tvel = cvel.normalized * currentYSpeed;
        _rigidbody2D.velocity = tvel;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "wall")
        {
            
             _rigidbody2D.AddForce(coll.GetContact(0).normal * currentYSpeed);
        }
    }
}
