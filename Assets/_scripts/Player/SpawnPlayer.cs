using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    /// <summary>
    /// Number of players to add.
    /// </summary>
    [SerializeField, Range(2, 4)]
    private int numOfPlayers = 0;

    /// <summary>
    /// Player to add.
    /// </summary>
    [SerializeField]
    private Player[] players = new Player[4];

    #region  Unity Methods


    /// <summary>
    /// START
    /// </summary>
    private void Start()
    {
        SpawnPlayers();
    }

    #endregion

    /// <summary>
    /// Spawns players from list.
    /// </summary>
    private void SpawnPlayers()
    {
        for (int i = 0; i < numOfPlayers; i++)
        {
            var player = players[i];
            if (player == null)
            {
                Debug.Log(string.Format("Player {0} is not added to script.", i + 1));
            }
            else
            {
                player.Create();
            }
        }
    }

   
}
