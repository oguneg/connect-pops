using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tile : MonoBehaviour
{
    private const float mergeDuration = 0.2f;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TMPro.TextMeshPro valueText;
    private int value;
    public int Value => value;
    private Slot currentSlot;

    public void AssignToSlot(Slot slot)
    {
        currentSlot = slot;
        currentSlot.SetLineRendererColor(image.color);
    }

    public void AssignData(TileData data)
    {
        value = data.value;
        image.color = data.color;
        valueText.text = data.valueString;
        currentSlot?.SetLineRendererColor(image.color);
    }

    public void IncreaseValue(TileData data)
    {
        DOVirtual.DelayedCall(mergeDuration, () =>
        AssignData(data));
    }

    public void MergeInto(Slot slot)
    {
        currentSlot?.ClearTile();
        transform.DOMove(slot.transform.position, mergeDuration).SetEase(Ease.InOutSine).OnComplete(Cleanup);
        valueText.DOFade(0, mergeDuration / 2f);
    }

    public void Cleanup()
    {
        currentSlot?.ClearTile();
        TileManager.instance.DeactivateTile(this);
        gameObject.SetActive(false);
        valueText.alpha = 1;
    }
}