using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    private const int highestSpawnValueOffset = 3;
    private Slot[,] grid;
    private Vector2Int gridSize;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Slot slotPrefab;
    private TileManager tileManager;
    private int slotCount;
    private int[] gameData;
    private int highCap;
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
        foreach (var element in grid)
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
                var tile = tileManager.SpawnTile(gameData[y * gridSize.y + x], true);
                tile.AssignToSlot(grid[x, y]);
                grid[x, y].AssignTile(tile);
            }
        }
    }
    public void RecalculateGrid()
    {
        HandleVerticalSpaces();
        FillEmptySlots();
    }

    private void HandleVerticalSpaces()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var bottomSlot = grid[x, y];

                if (bottomSlot.tile == null)
                {
                    for (int i = y + 1; i < gridSize.y; i++)
                    {
                        var topSlot = grid[x, i];
                        if (topSlot.tile != null)
                        {
                            topSlot.TransferTile(bottomSlot);
                            break;
                        }
                    }
                }
            }
        }
    }
    private void FillEmptySlots()
    {
        highCap = Mathf.Max(tileManager.HighestValue - highestSpawnValueOffset, 4);
        bool willEnsureMatch = !HasMatches();

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var slot = grid[x, y];
                if (slot.tile == null)
                {
                    var tile = tileManager.SpawnTile(willEnsureMatch ? GetNeighborValue(x, y) : RandomValue);
                    slot.AssignTile(tile);
                    tile.AssignToSlot(slot);
                    willEnsureMatch = false;
                }
            }
        }
    }

    private int RandomValue
    {
        get
        {
            return Random.Range(0, highCap);
        }
    }

    private bool HasMatches()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (HasMatchingNeighbor(x, y))
                {
                    return true;
                }
            }
        }
        Debug.Log($"no valid moves, ensured a match");
        return false;
    }

    private bool HasMatchingNeighbor(int x, int y)
    {
        if (grid[x, y].tile == null) return false;
        var value = grid[x, y].tile.Value;
        var neighbors = GetNeighbors(x, y);

        foreach (var element in neighbors)
        {
            if (element.tile != null && element.tile.Value == value)
            {
                return true;
            }
        }
        return false;
    }

    private int GetNeighborValue(int x, int y)
    {
        return GetNeighbors(x, y).GetRandom().tile.Value;
    }

    private List<Slot> GetNeighbors(int x, int y)
    {
        List<Slot> validNeighbors = new List<Slot>();
        if (x < gridSize.x - 1)
        {
            if (grid[x + 1, y].tile)
                validNeighbors.Add(grid[x + 1, y]);

            if (y > 0)
            {
                if (grid[x + 1, y - 1].tile)
                    validNeighbors.Add(grid[x + 1, y - 1]);
            }

            if (y < gridSize.y - 1)
            {
                if (grid[x + 1, y + 1].tile)
                    validNeighbors.Add(grid[x + 1, y + 1]);
                if (grid[x, y + 1].tile)
                    validNeighbors.Add(grid[x, y + 1]);
            }
        }
        else if (y < gridSize.y - 1)
        {
            if (grid[x, y + 1].tile)
                validNeighbors.Add(grid[x, y + 1]);
        }
        return validNeighbors;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveData();
    }

    private void SaveData()
    {
        gameData = new int[gridSize.x * gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                gameData[y * gridSize.y + x] = grid[x, y].tile.Value;
            }
        }
        DataHandler.SaveGameData(gameData);
    }
}