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


    public static (int x, int y) RoundVectorToInt(Vector2 vector)
    {
        var x = Mathf.RoundToInt(vector.x);
        var y = Mathf.RoundToInt(vector.y);
        return (x, y);
    }
    
    public static int DeltaX(Vector2 pos1, Vector2 pos2) => RoundVectorToInt(pos1 - pos2).x;
    public static int DeltaX(Transform tr1, Transform tr2) => DeltaX(tr1.position, tr2.position);

    public static float OrthoMagnitude(Vector2 vector) => Mathf.RoundToInt(Mathf.Abs(vector.x) + Mathf.Abs(vector.y));
    public static float OrthoDistance(Vector2 pos1, Vector2 pos2) => OrthoMagnitude(pos1 - pos2);
    public static float OrthoDistance(Transform tr1, Transform tr2) => OrthoDistance(tr1.position, tr2.position);
}