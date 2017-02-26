using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    [SerializeField, Range(2, 4)]
    private int numOfPlayers;

    [SerializeField]
    private Player[] players = new Player[4];


    #region  Unity Methods

    private void Start()
    {
        SpawnPlayers();
    }

    #endregion

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
                var position = new Vector3(UnityEngine.Random.Range(0, Screen.width), Screen.height, -1);
                Instantiate(player, position, Quaternion.identity);

            }
        }

    }
}
