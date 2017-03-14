

using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    /// <summary>
    /// Gets and sets spawn time.
    /// </summary>
    [SerializeField]
    private float spawnTime = Constants.PickUpSpawnTime;

    /// <summary>
    /// Object to spawn.
    /// </summary>
    [SerializeField]
    private List<GameObject> spawnObjects = new List<GameObject>();

    /// <summary>
    /// Unity start method.
    /// </summary>
    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    /// <summary>
    /// Unity update method.
    /// </summary>
    private void Update()
    {

    }

    #region Methods

    /// <summary>
    /// Spawn pickup item.
    /// </summary>
    private void Spawn()
    {
        if (spawnObjects.Count > 0)
        {
            var rand = UnityEngine.Random.Range(0, spawnObjects.Count);
            Instantiate(spawnObjects[rand],
                 new Vector3(UnityEngine.Random.Range(0, Screen.width), Screen.height, -1),
                 Quaternion.identity);
        }
    }

    #endregion
}
