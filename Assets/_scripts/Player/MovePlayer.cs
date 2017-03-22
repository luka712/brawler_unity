﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float startingScaleX;

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


    private bool canJump = false;
    private Collider2D thisCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerIndex playerIndex;

    public Vector2 Direction { get; private set; }

    #region Unity Methods

    /// <summary>
    /// Unity start
    /// </summary>
    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerIndex = GetComponent<Player>().PlayerIndex;

        jumpStrength *= Constants.JumpStrengthMultipler;

        startingScaleX = gameObject.transform.localScale.x;
    }

    /// <summary>
    /// Unity fixed update.
    /// </summary>
    private void FixedUpdate()
    {
        float directionX = 0;
        if(playerIndex == PlayerIndex.One)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                directionX = -1;
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                directionX = 1;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else if(playerIndex == PlayerIndex.Two)
        {
            directionX = Input.GetAxis(Axis.Horizontal);

            if (Input.GetButtonDown(Buttons.Player2Jump))
            {
                Jump();
            }
        }

        if(directionX > 0)
        {
            directionX = 1;
        }
        else if(directionX < 0)
        {
            directionX = -1;
        }

        animator.SetFloat(AnimationNames.Running, Mathf.Abs(directionX));
        float direction = directionX * Time.deltaTime * speed;
        this.transform.Translate(Vector3.right * direction);

        SwitchDirection(directionX);
    }

    /// <summary>
    /// On collision with other object.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Tile")
        {
            var contanctPoint = coll.contacts[0].point;
            if (this.thisCollider.bounds.center.y >= contanctPoint.y)
            {
                canJump = true;
                animator.SetBool(AnimationNames.Jumping, false);
            }
        }

    }

    #endregion Unity Methods

    #region Methods

    /// <summary>
    /// Player jump code.
    /// </summary>
    private void Jump()
    {
        if ( canJump)
        {
            rb.AddForce(Vector2.up * jumpStrength);
            canJump = false;
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
