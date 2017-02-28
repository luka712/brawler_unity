using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 400f;
    [SerializeField]
    private float jumpStrength = 3f;


    private bool canJump = false;
    private Collider2D thisCollider;
    private Rigidbody2D rb;
    private Animator animator;

    #region Unity Methods

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        jumpStrength *= Constants.JumpStrengthMultipler;
    }

    private void FixedUpdate ()
    {
        float direction = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        this.transform.Translate(Vector3.right * direction);

        Jump();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Tile")
        {

            var contanctPoint = coll.contacts[0].point;
            if(this.thisCollider.bounds.center.y >= contanctPoint.y)
            {
                canJump = true;
            }
        }

    }

    #endregion Unity Methods

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector2.up * jumpStrength);
            canJump = false;
            animator.SetBool("jumping", true);
        }
    }

}
