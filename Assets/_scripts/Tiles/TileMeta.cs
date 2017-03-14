using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeta
{ 
    /// <summary>
    /// Creates tile meta data.
    /// </summary>
    /// <param name="x">Tile x position.</param>
    /// <param name="y">Tile y position.</param>
    /// <param name="isEmptyPosition">Is Empty field</param>
    public TileMeta(int x, int y, bool isEmptyPosition)
    {
        X = x;
        Y = y;
        IsEmpty = isEmptyPosition;
    }

    public int X { get; private set; }
    public int Y { get; private set; }
    public bool IsEmpty { get; private set; }
}
