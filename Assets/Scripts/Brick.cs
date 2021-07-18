using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.ParticleSystem;
public class Brick : MonoBehaviour
{
    public static event Action<Brick> OnBrickDestruction;
    public int HitPoints = 1;
    public ParticleSystem DestroyEffect;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
        this._spriteRenderer.sprite = BricksManager.Instance.Sprites[this.HitPoints - 1];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }
    private void ApplyCollisionLogic(Ball ball)
    {
        this.HitPoints--;
        if (this.HitPoints <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);

        }
        else
        {
            this._spriteRenderer.sprite = BricksManager.Instance.Sprites[this.HitPoints - 1];
        }
    }
    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this._spriteRenderer.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }
}
