using System;
using UnityEngine;

public static class Utility
{
    private enum Axis
    {
        X, Y, Z
    }

    private static void SetCoordinate(Transform transform, Axis axis, float value)
    {
        var pos = transform.position;
        switch (axis)
        {
            case Axis.X:
                pos.x = value;
                break;
            case Axis.Y:
                pos.y = value;
                break;
            case Axis.Z:
                pos.z = value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
        }
        transform.position = pos;
    }

    public static void SetX(Transform transform, float x) => SetCoordinate(transform, Axis.X, x);
    public static void SetY(Transform transform, float y) => SetCoordinate(transform, Axis.Y, y);
}