using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _lightningBallDuration = 4;
    private Rigidbody2D _rigidbody2D;
    public ParticleSystem LightningBallEffect;
    public bool IsLightningBall;
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
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();  
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void MultiplySpeed(float factor)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x * factor, _rigidbody2D.velocity.y * factor);
    }
    public void StartLightningBall()
    {
        if (!IsLightningBall)
        {
            IsLightningBall = true;
            _spriteRenderer.enabled = false;
            LightningBallEffect.gameObject.SetActive(true);
            StartCoroutine(StopLightningBallAfterTime(_lightningBallDuration));
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


}
