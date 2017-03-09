using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> tiles;

    private List<GameObject> tileInstances = new List<GameObject>();
    private Tiles[] tileRows;

    #region Unity Methods

    private void Awake()
    {
        tileRows = TileMaps.DemoLevel;
        CreateTiles();
    }

    #endregion

    private void CreateTiles()
    {
        int height = 0;
        foreach (var row in tileRows)
        {
            int width = -Constants.TileSize;
            bool repeat = row.Repeating;
            height += Constants.TileSize;

            if (height > Screen.height) break;

            do
            {
                for (var i = 0; i < row.TileIndexes.Length; i++)
                {
                    width += Constants.TileSize;
                    var tile = tiles[row.TileIndexes[i]];
                    if (tile != null)
                    {
                        tileInstances.Add(
                            Instantiate<GameObject>(tile, new Vector3(width, height, -1), Quaternion.identity));
                    }
                    if (width > Screen.width)
                    {
                        repeat = false;
                        break;
                    }
                }
            } while (repeat);
        }
    }
}