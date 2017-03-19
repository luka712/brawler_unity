using System.Collections;
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

    #region Unity Methods

    /// <summary>
    /// Unity start
    /// </summary>
    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        jumpStrength *= Constants.JumpStrengthMultipler;

        startingScaleX = gameObject.transform.localScale.x;
    }

    /// <summary>
    /// Unity fixed update.
    /// </summary>
    private void FixedUpdate()
    {
        var directionX = Input.GetAxis(Axis.Horizontal);
        animator.SetFloat(AnimationNames.Running, Mathf.Abs(directionX));
        float direction = directionX * Time.deltaTime * speed;
        this.transform.Translate(Vector3.right * direction);

        Jump();
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
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
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
            Debug.Log(x);
            x = 1f;
        }
        else if (x < 0)
        {
            Debug.Log(x);
            x = -1f;
        }

        if (x == 1f || x == -1f)
        {
            var scale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(startingScaleX * x, scale.y, scale.z);
        }

    }

    #endregion

}
