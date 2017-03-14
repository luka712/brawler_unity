using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileSpawner : MonoBehaviour
{
    /// <summary>
    /// Tiles and respective indexes.
    /// </summary>
    [SerializeField]
    private List<GameObject> tiles;

    private IList<GameObject> tileInstances = new List<GameObject>();
    private Tiles[] tileRows;

    #region Unity Methods

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        tileRows = TileMaps.DemoLevel;
        CreateTiles();
    }

    #endregion

    /// <summary>
    /// Creates map tiles.
    /// </summary>
    private void CreateTiles()
    {
        int yPos = 0;
        foreach (var row in tileRows)
        {
            int xPos = -Constants.TileSize;
            bool repeat = row.Repeating;
            yPos += Constants.TileSize;

            if (yPos > Screen.height) break;

            do
            {
                for (var i = 0; i < row.TileIndexes.Length; i++)
                {
                    xPos += Constants.TileSize;
                    var tile = tiles[row.TileIndexes[i]];

                    if (tile != null)
                    {
                        tileInstances.Add(
                            Instantiate<GameObject>(tile, new Vector3(xPos, yPos, -1), Quaternion.identity));
                    }

                    if (xPos > Screen.width)
                    {
                        repeat = false;
                        break;
                    }
                }
            } while (repeat);
        }
    }
}