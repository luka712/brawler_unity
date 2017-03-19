using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Machinge Gun Bullet
/// </summary>
public class MachineGunBullet : BaseBullet
{
    /// <summary>
    /// Damage that bullets deals
    /// </summary>
    public override int Damage { get { return WeaponsConstants.MachineGunBulletDamage; } }

    /// <summary>
    /// Bullet start method.
    /// </summary>
    private void Start()
    {
        Speed = WeaponsConstants.MachineGunBulletSpeed;
    }

    /// <summary>
    /// Machinge gun bullet speed.
    /// </summary>
    private void Update()
    {
        gameObject.transform.Translate((direction * Speed).ToVector3());
    }

}
