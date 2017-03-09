﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Player : MonoBehaviour
{

    /// <summary>
    /// Gets or sets player health.
    /// </summary>
    [HideInInspector]
    private int health = Constants.PlayerHealth;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if(health > Constants.PlayerHealth)
            {
                health = Constants.PlayerHealth;
            }
            else if(health < 0)
            {
                health = 0;
            }
        }
    }

    /// <summary>
    /// Gets player lives.
    /// </summary>
    [HideInInspector]
    public int Lives { get; private set; }

    private DivideSprite spriteDivider;
    private SpawnPlayer playerSpawner;
    private PlayerInvicibleTimeAnimation spawnAnimation;
    private SpriteRenderer rend;

    #region Unity Methods

    /// <summary>
    /// START
    /// </summary>
    private void Start ()
    {
        spriteDivider = GetComponent<DivideSprite>();
        playerSpawner = this.gameObject.AddComponent<SpawnPlayer>();
        spawnAnimation = GetComponent<PlayerInvicibleTimeAnimation>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        Health = Constants.PlayerHealth;
        Lives = Constants.PlayerLives;
    }

    /// <summary>
    /// Unity collisions.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        // This will be used for game over transition
        bool hasSpawned;

        // No need to check if dead.
        if (Health <= 0) return;

        if (!spawnAnimation.IsPlaying)
        {
            if (coll.gameObject.tag == "RotPlatform")
            {
                var platform = coll.gameObject.GetComponent<Platform>();
                this.Health -= platform.Damage;
                if (Health <= 0)
                {
                    var dir = this.transform.position.ToVector2() - coll.contacts.First().point;
                    ExplodePlayerToPieces(dir);
                    PlayerDied();
                    Spawn(out hasSpawned);
                }
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Decrements lives and sets player to inactive state
    /// </summary>
    public virtual void PlayerDied()
    {
        Lives--;
    }

    /// <summary>
    /// Divides player sprite to small pieces.
    /// </summary>
    /// <param name="dir">Explode direction.</param>
    public virtual void ExplodePlayerToPieces(Vector2 dir)
    {
        spriteDivider.Divide(dir, 5, 10);
    }

    /// <summary>
    /// Creates player.
    /// </summary>
    public void Create()
    {
        Instantiate(this);
        Reset();
        StartCoroutine(CourutineSpawn());
    }

    /// <summary>
    /// If player has lives, spawnes player
    /// </summary>
    /// <param name="hasSpawned">Result of spawn, true if player has spawn, false otherwise.</param>
    public void Spawn(out bool hasSpawned)
    {
        if(Lives > 0)
        {
            Lives--;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0f);
            StartCoroutine(CourutineSpawn());
            hasSpawned = true;
        }
        hasSpawned = false;
    }

    /// <summary>
    /// If player has lives, spawnes player.
    /// </summary>
    /// <returns>Return spawn result. True if player will be spawned.</returns>
    public bool Spawn()
    {
        bool canSpawn;
        Spawn(out canSpawn);
        return canSpawn;
    }

    /// <summary>
    /// Spawns new player at random location and sets it to active.
    /// </summary>
    /// <param name="spawnTime">Spawn time.</param>
    private IEnumerator CourutineSpawn()
    {

        this.gameObject.transform.position = new Vector3(1000, 1000, this.transform.position.z);
        yield return new WaitForSeconds(Constants.PlayerSpawnTime);

        Reset();
    }

    /// <summary>
    /// Reset health and position.
    /// </summary>
    private void Reset()
    {
        this.Health = Constants.PlayerHealth;
        this.transform.position = new Vector3(UnityEngine.Random.Range(0, Screen.width), Screen.height, this.transform.position.z);
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 255f);
        spawnAnimation.Play();
    }

    #endregion
}
