using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnMap
{
    public static Tiles[] DemoLevel
    {
        get
        {
            return new Tiles[]{
                        // Bottom first
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,    
                        new Tiles(true, 1,1,1,1,1,1,1),
            };
        }
    }
}
