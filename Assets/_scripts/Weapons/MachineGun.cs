using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MachineGun : BaseWeapon
{
    private const int PoolItemsCount = 200;

    [SerializeField]
    private GameObject bulletPrefab;

    /// <summary>
    ///  Unity start.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        inactiveBullets = new List<BaseBullet>();
        activeBullets = new List<BaseBullet>();


        coolTime = WeaponsConstants.MachinGunCoolTime;
        for(int i = 0; i < PoolItemsCount; i++)
        {
            var bullet = Instantiate(bulletPrefab).GetComponent<MachineGunBullet>();
            bullet.gameObject.SetActive(false);
            inactiveBullets.Add(bullet);
        }
    }


    /// <summary>
    /// Shoots the bullets.
    /// </summary>
    public override void Shoot(Vector2 direction)
    {
        if(canShoot == true)
        {
            coolTimer = 0;
            var bullet = inactiveBullets.First();
            if(bullet != null)
            {
                inactiveBullets.RemoveAt(0);
                activeBullets.Add(bullet);
                bullet.gameObject.transform.position = gameObject.transform.position;
                bullet.Direction = direction;
                bullet.gameObject.SetActive(true);
            }
            
        }
    }
}
