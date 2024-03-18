using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Vector2Int pos;
    public Vector2Int Pos => pos;

    public void AssignPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }
}
