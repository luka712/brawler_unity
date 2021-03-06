﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Converts to vector2.
    /// </summary>
    public static Vector2 ToVector2(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    /// <summary>
    /// Converts to vector2.
    /// </summary>
    public static Vector2 ToVector2(this Vector3 vec, float x , float y)
    {
        return new Vector2(x, y);
    }

    /// <summary>
    /// Converts to Vector3.
    /// </summary>
    public static Vector3 ToVector3(this Vector2 vec)
    {
        return new Vector3(vec.x, vec.y, 0f);
    }

    /// <summary>
    /// Converts to Vector3.
    /// </summary>
    public static Vector3 ToVector3(this Vector2 vec, float z)
    {
        return new Vector3(vec.x, vec.y, z);
    }

    /// <summary>
    /// Sets alpha color.
    /// </summary>
    public static Color SetAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
