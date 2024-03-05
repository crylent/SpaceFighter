using System;
using UnityEngine;

[Serializable]
public struct LimitingRectangle
{
    public float left;
    public float right;
    public float top;
    public float bottom;

    public bool Check(Vector2 position) => 
        position.y <= top && position.y >= bottom && position.x >= left && position.x <= right;
}

[Serializable]
public struct Range
{
    public float min;
    public float max;

    public bool Check(float value) => value >= min && value <= max;
}