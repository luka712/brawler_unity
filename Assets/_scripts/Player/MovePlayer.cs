using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float startingScaleX;
    private int previousDirectionX;
    private bool collidingTileRight = false;
    private bool collidingTileLeft = false;

    /// <summary>
    /// Gets or set player movement speed
    /// </summary>
    [SerializeField]
    private float speed = 400f;

    /// <summary>
    /// Gets or set player jump strength
    /// </summary>
    [SerializeField]
    private float jumpStrength = 3f;


    private Collider2D thisCollider;
    private Collider2D groundCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerIndex playerIndex;

    public Vector2 Direction { get; private set; }
    public bool Grounded { get; set; }
    public bool AirControl { get; set; }

    #region Unity Methods

    /// <summary>
    /// Unity start
    /// </summary>
    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        groundCollider = GetComponentInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerIndex = GetComponent<Player>().PlayerIndex;

        jumpStrength *= Constants.JumpStrengthMultipler;

        startingScaleX = gameObject.transform.localScale.x;
        AirControl = true;
    }

    /// <summary>
    /// Unity fixed update.
    /// </summary>
    private void FixedUpdate()
    {
        float directionX = 0;
        if (playerIndex == PlayerIndex.One)
        {

            directionX = Input.GetAxis(Axis.Horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else if (playerIndex == PlayerIndex.Two)
        {
            directionX = Input.GetAxis(Axis.Horizontal);

            if (Input.GetButtonDown(Buttons.Player2Jump))
            {
                Jump();
            }
        }

        if (directionX > 0)
        {
            directionX = 1f;
        }
        else if (directionX < 0)
        {
            directionX = -1f;
        }

        float direction = directionX * speed;
        animator.SetFloat(AnimationNames.Running, Mathf.Abs(directionX));
        if (Grounded || AirControl)
        {
            if(!(directionX == 1f && collidingTileRight)
                && !(directionX == -1f && collidingTileLeft))
            {
                rb.velocity = new Vector2(direction, rb.velocity.y);
            }
        }

        SwitchDirection(directionX);
        previousDirectionX = (int)directionX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // stupid collisions fix
        if (collision.gameObject.tag == Tags.Tile)
        {
            var collisionPoint = collision.contacts[0];
            if (collisionPoint.point.x > thisCollider.transform.position.x)
            {
                collidingTileRight = true;
                collidingTileLeft = false;
            }
            else
            {
                collidingTileLeft = true;
                collidingTileRight = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Tags.Tile)
        {
            collidingTileRight = false;
            collidingTileLeft = false;
        }
    }

    #endregion Unity Methods

    #region Methods

    /// <summary>
    /// Player jump code.
    /// </summary>
    private void Jump()
    {
        if (Grounded)
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            Grounded = true;
            animator.SetBool(AnimationNames.Jumping, true);
        }
    }

    /// <summary>
    /// Changes x scale depending on direction.
    /// </summary>
    private void SwitchDirection(float x)
    {

        if (x > 0)
        {
            x = 1f;
        }
        else if (x < 0)
        {
            x = -1f;
        }

        if (x == 1f || x == -1f)
        {
            var scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(startingScaleX * x, scale.y, scale.z);
            Direction = new Vector2(x, 0);
        }

    }

    #endregion

}
