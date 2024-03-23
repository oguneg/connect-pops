using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slot : MonoBehaviour
{
    private Vector2Int pos;
    public Vector2Int Pos => pos;
    public Tile tile;
    [SerializeField] private LineRenderer lineRenderer;
    private bool isSelected;
    public bool IsSelected => isSelected;

    public void AssignTile(Tile tile)
    {
        this.tile = tile;
        tile.transform.position = transform.position;
        SetLineRendererColor();
    }

    public void AssignPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }

    public void InitializeLineRenderer()
    {
        //lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void SetLineRendererEnd(Slot otherSlot)
    {
        //lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, otherSlot.transform.position);
    }

    public void UpdateLineRendererEnd(Vector3 pos)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, pos);
    }

    public void SetLineRendererColor()
    {
        lineRenderer.startColor = lineRenderer.endColor = tile.GetColor();
    }

    public void HideLineRenderer()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(1, transform.position);
    }

    public void Select()
    {
        isSelected = true;
        tile.transform.DOScale(tile.transform.localScale * 1.15f, 0.1f).SetEase(Ease.OutBack);
    }

    public void Deselect()
    {
        isSelected = false;
        tile.transform.DOScale(tile.transform.localScale / 1.15f, 0.1f).SetEase(Ease.InBack);
        HideLineRenderer();
    }

    public bool IsNeighborOf(Vector2Int otherPos)
    {
        bool result = true;
        if (Mathf.Abs(otherPos.x - pos.x) > 1 || Mathf.Abs(otherPos.y - pos.y) > 1) result = false;
        if (otherPos == pos) result = false;
        return result;
    }
}
