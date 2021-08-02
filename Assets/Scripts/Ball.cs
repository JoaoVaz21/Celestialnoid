using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _lightningBallDuration = 4;
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
    }
    internal void StartLightningBall()
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
