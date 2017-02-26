﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaps 
{
    public static Tiles[] GetMap
    {
        get
        {
            return new Tiles[]{
                        // Bottom first
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        new Tiles(true, 1),
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        new Tiles(true, 0,0,0,0,0,2,2,2,2,2,0,0,0,0,0),
                        Tiles.Empty,
                        Tiles.Empty,
                        Tiles.Empty,
                        new Tiles(true, 2,2,2,2,2,0,0,0,0,0)
            };
        }
    }
}