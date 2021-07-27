using System;
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
    public const int MaxCollumnNumber = 13;
    public int CurrentMaxRowNumber = 4;
    private const float initialBrickSpawnPositionX = -2.4f;
    private const float initialBrickSpawnPositionY = 3.325f;
    private const float _xShiftAmount = 0.130f;
    private const float _yShiftAmount = 0.365f;
    private GameObject _bricksContainer;
    public Brick[] BrickPrefabs;
    private float[] _bricksXValue; 
    public static event Action OnLevelCompleted;

    public List<List<int>> LevelMap { get; private set; }
    public List<Brick> RemainingBricks { get; set; }
    public int InitialBricksCount { get; set; }

    public void LoadNextLevel()
    {
        this.LevelMap = GenerateLevelMap();
    }

    private void Start()
    {
        _bricksContainer = new GameObject("BricksContainer");
        _bricksXValue = new float[BrickPrefabs.Length];
        for (var i = 0; i < BrickPrefabs.Length; i++)
        {
            _bricksXValue[i] = BrickPrefabs[i].GetComponent<BoxCollider2D>().size.x;
        }

        LoadNextLevel();
        GenerateBricks();
        Brick.OnBrickDestruction += OnBrickDestruction;
    
    }

    private void OnBrickDestruction(Brick obj)
    {
        RemainingBricks.Remove(obj);
        if (RemainingBricks.Count == 0)
        {
            OnLevelCompleted?.Invoke();
        }
    }

    private List<List<int>> GenerateLevelMap()
    {
        var result = new List<List<int>>();
        for (var i =0;i< CurrentMaxRowNumber; i++)
        {
            var currentSize = 0;
            var maxType = BrickPrefabs.Length+1;
            var currentRow = new List<int>();
            while (currentSize < MaxCollumnNumber)
            {
                maxType = Mathf.Min(maxType, MaxCollumnNumber - currentSize);
                var brickType = UnityEngine.Random.Range(0, maxType);
                currentRow.Add(brickType);
                currentSize += brickType == 0 ? 1 : brickType;
            }
            result.Add(currentRow);
        }
        return result;
    }
    public void GenerateBricks()
    {
        RemainingBricks = new List<Brick>();
        float currentSpawnX = initialBrickSpawnPositionX;
        float currentSpawnY = initialBrickSpawnPositionY;
        float zShift = 0;
        int maxHitPoints = GameManager.Instance.MaxBrickHitPoints + 1;
        foreach (var row in LevelMap)
        {
            foreach(var brickType in row)
            {
                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(BrickPrefabs[brickType - 1], new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as Brick;
                    var hitPoints = UnityEngine.Random.Range(1,maxHitPoints);
                    newBrick.Init(_bricksContainer.transform, hitPoints);
                    this.RemainingBricks.Add(newBrick);
                    zShift += 0.0001f;
                }
                currentSpawnX += brickType == 0 ? _bricksXValue[0] + _xShiftAmount : _bricksXValue[brickType - 1] + _xShiftAmount;
            }
            currentSpawnY -= _yShiftAmount;
            currentSpawnX = initialBrickSpawnPositionX;

        }

        InitialBricksCount = RemainingBricks.Count;
    }
    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;

    }
}
