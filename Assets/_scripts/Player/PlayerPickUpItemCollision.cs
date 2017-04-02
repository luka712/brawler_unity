using Assets._scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
///     Player pick up item collider
/// </summary>
public class PlayerPickUpItemCollision : MonoBehaviour
{
    private Player player;
    private Animator animator;

    [SerializeField]
    private List<PickUpItemKeyValue> pickUps = new List<PickUpItemKeyValue>();

    #region Unity Methods

    /// <summary>
    ///     Start unity method.
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    /// <summary>
    ///     Unity collision method
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            var pickUpType = collision.gameObject.GetComponent<PickUpItem>().Type;

            Type type = null;
            switch (pickUpType)
            {
                case PickUpItemType.MachineGun:
                    type = typeof(MachineGun);
                    break;
            }

            if (type != null)
            {
                var go = gameObject.GetComponentInChildren(type, true);
                go.gameObject.SetActive(true);
                player.EquipedWeapon = (BaseWeapon)go;
                animator.SetBool(AnimationNames.HasWeapon, true);
            }

            Destroy(collision.gameObject);
        }
    }

    #endregion
}
