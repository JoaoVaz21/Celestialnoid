using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton
    private static BricksManager _instance;
    public static BricksManager Instance => _instance;
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
    public Sprite[] Sprites;

 
}
