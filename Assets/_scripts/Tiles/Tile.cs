using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles
{
    public bool Repeating { get; private set; }
    public int[] TileIndexes { get; private set; }

    public Tiles(bool repeating, params int[] indexes)
    {
        Repeating = repeating;
        TileIndexes = indexes;
    }

    public Tiles(params int[] indexes)
        :this(false, indexes)
    {
    }

    public static Tiles Empty
    {
        get { return new Tiles(new int[] { }); }
    }
}
