using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    private Slot[,] grid;
    private Vector2Int gridSize;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Slot slotPrefab;
    private TileManager tileManager;
    private int slotCount;
    private int[] gameData;

    public void Initialize(int[] data)
    {
        gameData = data;
        gridSize = GameManager.instance.GridSize;
        tileManager = TileManager.instance;
        CreateGrid();
        FillGrid();
    }

    private void CreateGrid()
    {
        grid = new Slot[gridSize.x, gridSize.y];
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                SpawnSlot(x, y);
            }
        }
        slotCount = gridSize.x * gridSize.y;
        gridParent.transform.position = new Vector3((gridSize.x - 1) * -0.5f, 5.5f - gridSize.y, -.5f);
        foreach(var element in grid)
        {
            element.InitializeLineRenderer();
        }
    }
    private void SpawnSlot(int x, int y)
    {
        var slot = Instantiate(slotPrefab, gridParent);
        grid[x, y] = slot;
        slot.AssignPos(x, y);
        slot.gameObject.name = $"Slot{x}x{y}";
        slot.transform.position = new Vector3(x, y, 0);
    }
    private void FillGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                var tile = tileManager.SpawnTile(gameData[y*gridSize.y + x]);
                tile.AssignToSlot(grid[x,y]);
                grid[x,y].AssignTile(tile);
            }
        }
    }

    private void RecalculateGrid()
    {

    }
}