using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    #region Singleton
    private static CollectablesManager _instance;
    public static CollectablesManager Instance => _instance;

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
    #endregion    // Start is called before the first frame update

    public List<Collectable> AvailableBuffs;
    public List<Collectable> AvailableDebuffs;
    [Range(0, 100)]
    public float BuffChance = 50;
    [Range(0, 100)]
    public float DebuffChance = 50;
}
