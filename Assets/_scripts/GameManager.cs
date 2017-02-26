using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private const int TileSize = 32;

    [SerializeField]
    private List<GameObject> tiles;
    [SerializeField, Range(2, 4)]
    private int numOfPlayers;

    private Material glowMaterial;
    private List<GameObject> tileInstances = new List<GameObject>();
    private Tiles[] tileRows =
    {
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

    #region Unity Methods

    private void Start()
    {
        glowMaterial = new Material(Shader.Find("Custom/glow"));
    }

    private void Awake()
    {
        SetCameraPosition();
        CreateTiles();
        SpawnPlayers();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, glowMaterial);
    }

    #endregion

    private void SetCameraPosition()
    {
        var mainCamera = Camera.main;
        mainCamera.orthographicSize = Screen.height * .5f;
        float halfTile = TileSize * .5f;
        mainCamera.transform.Translate(new Vector3(Screen.width * .5f - halfTile, Screen.height * .5f + halfTile, 0f));
    }

    private void CreateTiles()
    {
        int height = 0;
        foreach (var row in tileRows)
        {
            int width = -TileSize;
            bool repeat = row.Repeating;
            height += TileSize;

            if (height > Screen.height) break;

            do
            {
                for (var i = 0; i < row.TileIndexes.Length; i++)
                {
                    width += TileSize;
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

    private IEnumerable<string> GetPlayerTags()
    {
        for (int i = 1; i <= numOfPlayers; i++)
            yield return String.Format("Player{0}", i);
    }

   
    private void SpawnPlayers()
    {
        Resources.FindObjectsOfTypeAll(typeof(Player))
            .Cast<Player>()
            .Where(x => GetPlayerTags().Contains(x.tag))
            .ToList()
            .ForEach(x => 
                Instantiate(
                    x, 
                    new Vector3(UnityEngine.Random.Range(0, Screen.width ), Screen.height, -1), Quaternion.identity));
    }
}

