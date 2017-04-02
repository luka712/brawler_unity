using System.Collections;
using UnityEngine;
using System.Linq;
using System;

public delegate void HealthChange(int health);

public class Player : MonoBehaviour
{
    private DivideSprite spriteDivider;
    private PlayerInvicibleTimeAnimation spawnAnimation;
    private SpriteRenderer rend;
    private Rigidbody2D rigBody;
    private ClampToScreen clamp;
    private MovePlayer movePlayerScript;
    private float lastFacingDirection;

    /// <summary>
    /// Called when health has changed.
    /// </summary>
    public event HealthChange OnDamageApplied;

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
            if (health > Constants.PlayerHealth)
            {
                health = Constants.PlayerHealth;
            }
            else if (health < 0)
            {
                health = 0;
            }
            if (OnDamageApplied != null)
            {
                OnDamageApplied(health);
            }
        }
    }

    /// <summary>
    /// Gets player lives.
    /// </summary>
    [HideInInspector]
    public int Lives { get; private set; }

    /// <summary>
    /// Gets or sets equiped weapon.
    /// </summary>
    [HideInInspector]
    public BaseWeapon EquipedWeapon { get; set; }

    /// <summary>
    /// Gets player index.
    /// </summary>
    [SerializeField]
    private PlayerIndex playerIndex;
    public PlayerIndex PlayerIndex
    {
        get { return playerIndex; }
    }

    #region Unity Methods

    /// <summary>
    /// START
    /// </summary>
    private void Start()
    {
        spriteDivider = GetComponent<DivideSprite>();
        spawnAnimation = GetComponent<PlayerInvicibleTimeAnimation>();
        rend = GetComponent<SpriteRenderer>();
        rigBody = GetComponent<Rigidbody2D>();
        clamp = GetComponent<ClampToScreen>();
        movePlayerScript = GetComponent<MovePlayer>();
        Health = Constants.PlayerHealth;
        Lives = Constants.PlayerLives;
    }

    /// <summary>
    /// Unity update.
    /// </summary>
    private void Update()
    {
        var lastDirection = movePlayerScript.Direction.x;
        if(lastDirection > 0)
        {
            lastFacingDirection = 1;
        }
        else if(lastDirection < 0)
        {
            lastFacingDirection = -1;
        }

        if((Input.GetKey(KeyCode.LeftControl) && PlayerIndex == PlayerIndex.One)
            || (Input.GetButton(Buttons.Player2Shoot) && PlayerIndex == PlayerIndex.Two))
        {
            if(EquipedWeapon != null)
            {
                EquipedWeapon.Shoot(new Vector2(lastFacingDirection, 0));
            }
        }
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
            if (coll.gameObject.tag == Tags.RotatingPlatform)
            {
                var platform = coll.gameObject.GetComponent<Platform>();
                this.Health -= platform.Damage;
                var dir = this.transform.position.ToVector2() - coll.contacts.First().point;
                if (Health <= 0)
                {
                    Die(dir, out hasSpawned);
                }
                else
                {
                    rigBody.AddForce(new Vector2(
                        dir.x * UnityEngine.Random.Range(Constants.PlatformCollisionMinForceRange, Constants.PlatformCOllisionMaxForceRange),
                        dir.y * UnityEngine.Random.Range(Constants.PlatformCollisionMinForceRange, Constants.PlatformCOllisionMaxForceRange))
                        * Constants.ForceMultipler);
                }
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Creates player.
    /// </summary>
    public void Create(bool firstSpawn = false)
    {
        Reset();
        if (!firstSpawn)
        {
            StartCoroutine(CourutineSpawn());
        }
        else
        {
            Reset();
        }

    }

    /// <summary>
    /// Divides player sprite to small pieces.
    /// </summary>
    /// <param name="dir">Explode direction.</param>
    public virtual void ExplodePlayerToPieces(Vector2 dir)
    {
        spriteDivider.Divide(dir, 5, 10);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// If player has lives, spawnes player
    /// </summary>
    /// <param name="hasSpawned">Result of spawn, true if player has spawn, false otherwise.</param>
    private void Spawn(out bool hasSpawned)
    {
        if (Lives > 0)
        {
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
    private bool Spawn()
    {
        bool canSpawn;
        Spawn(out canSpawn);
        return canSpawn;
    }

    /// <summary>
    /// Player died method. Explosion direction. Out true if spawned.
    /// </summary>
    public void Die(Vector2 dir, out bool hasSpawned)
    {
        if (spawnAnimation.IsPlaying)
        {
            hasSpawned = false;
            return;
        }
        ExplodePlayerToPieces(dir);
        Lives--;
        Spawn(out hasSpawned);
    }


    /// <summary>
    /// Spawns new player at random location and sets it to active.
    /// </summary>
    /// <param name="spawnTime">Spawn time.</param>
    private IEnumerator CourutineSpawn()
    {

        this.gameObject.transform.position = new Vector3(10000, -10000, this.transform.position.z);
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
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1f);
        spawnAnimation.Play();
        clamp.ApplyClamp = true;
    }

   
    #endregion
}