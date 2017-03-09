using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles
{
    /// <summary>
    /// Gets tile repeating pattern.
    /// </summary>
    public bool Repeating { get; private set; }

    /// <summary>
    /// Gets tile indexes.
    /// </summary>
    public int[] TileIndexes { get; private set; }

    /// <summary>
    /// Create tiles.
    /// </summary>
    /// <param name="repeating">Is repeating ? </param>
    /// <param name="indexes">Tiles indexes.</param>
    public Tiles(bool repeating, params int[] indexes)
    {
        Repeating = repeating;
        TileIndexes = indexes;
    }

    /// <summary>
    /// Sets indexes without repating pattern.
    /// </summary>
    public Tiles(params int[] indexes)
        :this(false, indexes)
    {
    }

    /// <summary>
    /// Empty row.
    /// </summary>
    public static Tiles Empty
    {
        get { return new Tiles(new int[] { }); }
    }
}
