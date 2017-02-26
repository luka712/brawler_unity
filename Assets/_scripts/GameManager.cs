using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private Material glowMaterial;

    #region Unity Methods

    private void Start()
    {
        glowMaterial = new Material(Shader.Find("Custom/glow"));
    }

    private void Awake()
    {
        SetCameraPosition();
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
        float halfTile = Constants.TileSize * .5f;
        mainCamera.transform.Translate(new Vector3(Screen.width * .5f - halfTile, Screen.height * .5f + halfTile, 0f));
    }
}

