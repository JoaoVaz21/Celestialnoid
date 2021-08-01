using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.ParticleSystem;
using Assets.Scripts;

public class Brick : MonoBehaviour
{
    public static event Action<Brick> OnBrickDestruction;
    public int CurrentHitPoints = 1;
    public ParticleSystem DestroyEffect;
    public Sprite[] Sprites;
    public int Dificulty { get; set; }
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }
    private void ApplyCollisionLogic(Ball ball)
    {
        this.CurrentHitPoints--;
        if (this.CurrentHitPoints <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            OnBrickDestroy();
            Destroy(this.gameObject);

        }
        else
        {
            this._spriteRenderer.sprite = Sprites[this.CurrentHitPoints - 1];
        }
    }

    private void OnBrickDestroy()
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float debuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;
        if(buffSpawnChance <= CollectablesManager.Instance.BuffChance)
        {
            alreadySpawned = true;
            SpawnCollectable(true);
        }
        if(debuffSpawnChance<=CollectablesManager.Instance.DebuffChance && !alreadySpawned)
        {
            SpawnCollectable(false);
        }

        SpawnDestroyEffect();

    }

    private void SpawnCollectable(bool isBuff)
    {
        List<Collectable> collection = isBuff ? CollectablesManager.Instance.AvailableBuffs : CollectablesManager.Instance.AvailableDebuffs;
        int buffIndex = UnityEngine.Random.Range(0, collection.Count);
        var prefab = collection[buffIndex];
        Instantiate(prefab, this.transform.position, Quaternion.identity);
      
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        var brickSize = GetComponent<BoxCollider2D>().size;
        Vector3 spawnPosition = new Vector3(brickPos.x+brickSize.x/2, brickPos.y-brickSize.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this._spriteRenderer.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }
    public void Init(Transform containeTtransform, int hitPoints)
    {
        this.transform.SetParent(containeTtransform);
        _spriteRenderer.sprite = Sprites[hitPoints-1];
        CurrentHitPoints = hitPoints;
        Dificulty = hitPoints;
    }
}
