using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    private int damage;

    /// <summary>
    /// Gets weapon damage
    /// </summary>
    public int Damage { get { return damage; } }

    /// <summary>
    /// Unity start.
    /// </summary>
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
