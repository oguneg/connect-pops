using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Cleanup()
    {

    }
}