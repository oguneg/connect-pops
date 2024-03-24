using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    [SerializeField] private TilePool tilePool;
    private TileData[] tileData;
    private Color[] pallette;
    [SerializeField] private Tile sumIndicator;
    private int highestValue;
    public int HighestValue => highestValue;

    public void Initialize(Color[] pallette)
    {
        this.pallette = pallette;
        GenerateTileData();
    }

    public Tile SpawnTile(int value, bool isFirstSpawn = false)
    {
        if (value > highestValue)
        {
            highestValue = value;
        }
        var tile = tilePool.GetTile();
        tile.transform.localScale = Vector3.zero;
        tile.gameObject.SetActive(true);
        if (isFirstSpawn)
        {
            tile.transform.localScale = Vector3.one;
        }
        else
        {
            tile.transform.DOScale(1f, 0.25f).SetDelay(UnityEngine.Random.Range(0.45f, 0.55f)).SetEase(Ease.OutBack);
        }
        tile.AssignData(tileData[value]);
        return tile;
    }

    public void IncreaseTileValue(Tile tile)
    {
        tile.IncreaseValue(tileData[sumIndicator.Value]);
        if(sumIndicator.Value > highestValue)
        {
            highestValue = sumIndicator.Value;
        }
    }

    public void SetSumIndicatorValue(int value)
    {
        if (sumIndicator.Value != value)
            sumIndicator.AssignData(tileData[value]);
    }

    public void SetSumIndicatorStatus(bool status)
    {
        sumIndicator.gameObject.SetActive(status);
    }

    public void DeactivateTile(Tile tile)
    {
        tilePool.DeactivateTile(tile);
    }

    public void GenerateTileData()
    {
        int dataCap = 80;
        string[] numbers = { "2", "4", "8", "16", "32", "64", "128", "256", "512", "1" };
        string[] abbreviations = { "", "K", "M", "B", "T", "q", "Q", "s", "S", "o", "n", "d", "U" };

        tileData = new TileData[dataCap];
        for (int i = 0; i < dataCap; i++)
        {
            TileData data;
            data.color = pallette[i % 10];
            data.value = i;
            data.valueString = CalculateString(i);
            data.isEven = (i + 1) / 10 % 2 == 0;
            tileData[i] = data;
        }

        string CalculateString(int i)
        {
            return $"{numbers[i % 10]}{abbreviations[(i + 1) / 10]}";
        }
    }
}

public struct TileData
{
    public int value;
    public Color color;
    public string valueString;
    public bool isEven;
}