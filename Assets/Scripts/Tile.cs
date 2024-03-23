using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private TMPro.TextMeshPro valueText;
    private int value;
    public int Value => value;

    public void AssignData(TileData data)
    {
        value = data.value;
        image.color = data.color;
        valueText.text = data.valueString;
    }

    public Color GetColor()
    {
        return image.color;
    }

    public void IncreaseValue(TileData data)
    {
        DOVirtual.DelayedCall(0.2f, () =>
        AssignData(data));
    }

    public void MergeInto(Slot slot)
    {
        transform.DOMove(slot.transform.position, 0.2f).SetEase(Ease.InOutSine).OnComplete(Cleanup);
        valueText.DOFade(0, 0.1f);
    }

    public void Cleanup()
    {
        gameObject.SetActive(false);
        valueText.alpha = 1;
    }
}