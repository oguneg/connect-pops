using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slot : MonoBehaviour
{
    private Vector2Int pos;
    public Vector2Int Pos => pos;
    public Tile tile;

    public void AssignTile(Tile tile)
    {
        this.tile = tile;
        tile.transform.position = transform.position;
    }

    public void AssignPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }

    public void Select()
    {
        tile.transform.DOScale(1.1f,0.2f).SetRelative().SetEase(Ease.OutBack);
    }
}
