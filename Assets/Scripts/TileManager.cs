using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    [SerializeField] private TilePool tilePool;
    private TileData[] tileData;
    private Color[] pallette;
    public void Initialize(Color[] pallette)
    {
        this.pallette = pallette;
        GenerateTileData();
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