using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour {

    private MovePlayer player;
    private Animator animator;

    private void Start()
    {
        player = GetComponentInParent<MovePlayer>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            player.Grounded = true;
            player.AirControl = true;
            animator.SetBool(AnimationNames.Jumping, false);
        }
    }
}
