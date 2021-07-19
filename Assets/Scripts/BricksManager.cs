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
    public const int MaxCollumnNumber = 7;
    public int CurrentMaxRowNumber = 4;
    private const float initialBrickSpawnPositionX = -2.12f;
    private const float initialBrickSpawnPositionY = 3.325f;
    private const float _xShiftAmount = 0.700f;
    private const float _yShiftAmount = 0.365f;

    private GameObject _bricksContainer;
    public Sprite[] Sprites;
    public Brick BrickPrefab; 
    
    public int[,] LevelMap { get; private set; }
    public List<Brick> RemainingBricks { get; set; }
    public int InitialBricksCount { get; set; }


    private void Start()
    {
        _bricksContainer = new GameObject("BricksContainer");
        RemainingBricks = new List<Brick>();
        LevelMap = GenerateLevelMap();
        GenerateBricks();
    }

    private int[,] GenerateLevelMap()
    {
        var result = new int[CurrentMaxRowNumber, MaxCollumnNumber];
        for (var i =0;i< CurrentMaxRowNumber; i++)
        {
            for (var j = 0; j < MaxCollumnNumber; j++)
            {
                result[i, j] = Random.Range(0,4);
            }
        }
        return result;
    }
    private void GenerateBricks()
    {
        float currentSpawnX = initialBrickSpawnPositionX;
        float currentSpawnY = initialBrickSpawnPositionY;
        float zShift = 0;
        for(var row = 0; row < CurrentMaxRowNumber; row++)
        {
            for(var col = 0; col<MaxCollumnNumber; col++)
            {
                int brickType = LevelMap[row, col];
                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(BrickPrefab,new Vector3(currentSpawnX,currentSpawnY,0.0f-zShift),Quaternion.identity) as Brick;
                    newBrick.Init(_bricksContainer.transform, this.Sprites[brickType - 1],brickType);
                    this.RemainingBricks.Add(newBrick);
                    zShift += 0.0001f;
                }
                currentSpawnX += _xShiftAmount;
                if(col+1 == MaxCollumnNumber)
                {
                    currentSpawnX = initialBrickSpawnPositionX;
                }
            }
            currentSpawnY -= _yShiftAmount;
        }
        InitialBricksCount = RemainingBricks.Count;
    }
}
