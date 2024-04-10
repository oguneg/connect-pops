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

    public void AssignTile(Tile tile, bool isInstant = true)
    {
        this.tile = tile;
        tile.transform.SetParent(transform);
        if (isInstant)
        {
            tile.transform.localPosition = Vector3.zero;
        }
        else
        {
            tile.transform.DOLocalMove(Vector3.zero, 6).SetSpeedBased().SetEase(Ease.InCirc).SetDelay(Random.Range(0.08f, 0.12f)).OnComplete(() => tile.Squish());
        }
    }

    public void ClearTile()
    {
        tile = null;
    }

    public void TransferTile(Slot target)
    {
        target.AssignTile(tile, false);
        tile.AssignToSlot(target);
        ClearTile();
    }

    public void AssignPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }

    public void InitializeLineRenderer()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void SetLineRendererEnd(Slot otherSlot)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, otherSlot.transform.position);
    }

    public void SetLineRendererColor(Color color)
    {
        //lineRenderer.startColor = lineRenderer.endColor = color;
        lineRenderer.material.color = color;
    }

    public void HideLineRenderer()
    {
        lineRenderer.enabled = false;
        //lineRenderer.SetPosition(1, transform.position);
    }

    public void Select()
    {
        isSelected = true;
        tile.transform.DOScale(tile.transform.localScale * 1.15f, 0.1f).SetEase(Ease.OutBack);
    }

    public void Deselect()
    {
        isSelected = false;
        tile?.transform.DOScale(tile.transform.localScale / 1.15f, 0.1f).SetEase(Ease.InBack);
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
