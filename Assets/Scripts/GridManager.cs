using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    private Slot[,] grid;
    private Vector2Int gridSize;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Slot slotPrefab;
    private int slotCount;

    public void Initialize()
    {
        gridSize = GameManager.instance.GridSize;
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
    }
    private void SpawnSlot(int x, int y)
    {
        var slot = Instantiate(slotPrefab, gridParent);
        grid[x, y] = slot;
        slot.AssignPos(x, y);
        slot.transform.position = new Vector3(x, y, 0);
    }
    private void FillGrid()
    {

    }

    private void RecalculateGrid()
    {

    }
}