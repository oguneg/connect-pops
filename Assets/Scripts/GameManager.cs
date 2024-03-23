using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Vector2Int gridSize = new Vector2Int(5, 5);
    public Vector2Int GridSize => gridSize;
    private int slotCount;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private InputHandler inputHandler;

    private int[] gameData;

    private void Start()
    {
        Application.targetFrameRate = 60;
        FetchGameSettings();
        FetchGameData();
        tileManager.Initialize(gameSettings.colorPallette);
        gridManager.Initialize(gameData);
        inputHandler.Initialize();
    }

    private void Update()
    {
        inputHandler.Tick();
    }

    private void FetchGameData()
    {
        if (DataHandler.IsNewPlayer)
        {
            GenerateRandomNewGameData();
            DataHandler.IsNewPlayer = false;
        }
        else
        {
            gameData = DataHandler.LoadGameData();
            if (gameData.Length == 0)
            {
                Debug.LogError("Game Data Error");
            }
        }
    }

    private void GenerateRandomNewGameData()
    {
        gameData = new int[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            gameData[i] = Random.Range(0, 4);
        }
        DataHandler.SaveGameData(gameData);
    }

    private void FetchGameSettings()
    {
        if (gameSettings != null)
        {
            gridSize = gameSettings.gridSize;
        }
        slotCount = gridSize.x * gridSize.y;
    }
}
