using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cell
{
    // 格子坐标
    private Point point;
    public int X => point.X;
    public int Y => point.Y;
    // 是否可以放塔
    private bool isTowerPos;

    public BaseTower tower;

    public bool IsTowerPos
    {
        get => isTowerPos;
        set => isTowerPos = value;
    }

    public Cell(Point point)
    {
        this.point = point;
    }

    public override string ToString()
    {
        return $"x:{X},y:{Y}";
    }
}
