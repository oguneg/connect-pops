using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public List<Tile> inactiveTiles = new List<Tile>();
    [SerializeField] private Tile tilePrefab;
    private int totalTileCount;
    public Tile GetTile()
    {
        if(inactiveTiles.Count > 0)
        {
            var element = inactiveTiles[0];
            inactiveTiles.Remove(element);
            return element;
        }
        else
        {
            return SpawnTile();
        }
    }

    private Tile SpawnTile()
    {
        totalTileCount++;
        var tile = Instantiate(tilePrefab);
        return tile;
    }

    public void DeactivateTile(Tile tile)
    {
        inactiveTiles.Add(tile);
    }
}
