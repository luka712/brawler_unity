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
            var pickUp = pickUps
                .FirstOrDefault(x => x.Type == pickUpType);

            Type type = null;
            switch (pickUpType)
            {
                case PickUpItemType.MachineGun:
                    type = typeof(MachineGun);
                    break;
            }

            if(type != null)
            {
                var go = gameObject.GetComponentInChildren(type, true);
                go.gameObject.SetActive(true);
                animator.SetBool(AnimationNames.HasWeapon, true);
            }

        }
    }

    #endregion
}
