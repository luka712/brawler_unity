using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private float initialSpawnTime = 2f;
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
        StartCoroutine(SpawnPlayers(true));
    }

    #endregion

    /// <summary>
    /// Spawns players from list.
    /// </summary>
    private IEnumerator SpawnPlayers(bool firstSpawn = false)
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < numOfPlayers; i++)
        {
            if(i >= players.Length)
            {
                break;
            }

            var player = players[i];
            if (player == null)
            {
                Debug.Log(string.Format("Player {0} is not added to script.", i + 1));
            }
            else
            {
                player.Create(firstSpawn);
            }
        }
    }

   
}
