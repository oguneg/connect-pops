using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    private const float mergeDuration = 0.2f;
    [SerializeField] private SpriteRenderer image, ring;
    [SerializeField] private TMPro.TextMeshPro valueText;
    [SerializeField] private Transform visual;
    private int value;
    public int Value => value;
    private Slot currentSlot;
    public UnityAction<Tile> OnMergeComplete;

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
        ring.enabled = !data.isEven;
        currentSlot?.SetLineRendererColor(image.color);
    }

    public void Squish()
    {
        visual.DOScaleY(0.8f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    public void IncreaseValue(TileData data)
    {
        DOVirtual.DelayedCall(mergeDuration, () =>
        {
            AssignData(data);
            transform.DOScale(1.3f, 0.15f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
            OnMergeComplete?.Invoke(this);
        });
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
        currentSlot = null;
        TileManager.instance.DeactivateTile(this);
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
        valueText.alpha = 1;
    }
}