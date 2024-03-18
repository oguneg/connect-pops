using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Scriptable Object/Game Settings")]
public class GameSettings : ScriptableObject
{
    public Vector2Int gridSize;
    public Color[] colorPallette;
}
