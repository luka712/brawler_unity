using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollision : MonoBehaviour
{

    private Player player;

    /// <summary>
    /// Unity start.
    /// </summary>
    private void Start()
    {
        player = GetComponent<Player>();
    }

    /// <summary>
    /// Unity collision.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            var bullet = collision.gameObject.GetComponent<BaseBullet>();
            if(bullet != null && bullet.PlayerIndex != player.PlayerIndex)
            {
                player.Health -= bullet.Damage;
                if (player.Health <= 0)
                {
                    bool spawned = false;
                    player.Die(collision.contacts[0].normal, out spawned);
                }
                bullet.gameObject.SetActive(false);
            }
        }
    }
}
