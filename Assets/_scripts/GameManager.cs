using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Game manager class.
/// </summary>
public class GameManager : MonoBehaviour
{
    private Material glowMaterial;

    #region Unity Methods

    /// <summary>
    /// Unity start method.
    /// </summary>
    private void Start()
    {
        glowMaterial = new Material(Shader.Find("Custom/glow"));
    }

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        SetCameraPosition();
    }

    /// <summary>
    /// Render image. For post processing effects.
    /// </summary>
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, glowMaterial);
    }

    #endregion

    /// <summary>
    /// Sets camera position.
    /// </summary>
    private void SetCameraPosition()
    {
        var mainCamera = Camera.main;
        mainCamera.orthographicSize = Screen.height * .5f;
        float halfTile = Constants.TileSize * .5f;
        mainCamera.transform.Translate(new Vector3(Screen.width * .5f - halfTile, Screen.height * .5f + halfTile, 0f));
    }
}

