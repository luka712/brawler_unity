using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MachineGun : BaseWeapon
{
    private const int BulletPositionOffset = 10;
    private const int PoolItemsCount = 200;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject forwardMuzzleFlash;
    private MuzzleFlash forwardMuzzleFlashScript;
    [SerializeField]
    private GameObject upMuzzleFlash;
    private MuzzleFlash upMuzzleFlashScript;
    [SerializeField]
    private GameObject downMuzzleFlash;
    private MuzzleFlash downMuzzleFlashScript;

    private ParticleSystem emitter;


    /// <summary>
    ///  Unity start.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        inactiveBullets = new List<BaseBullet>();
        activeBullets = new List<BaseBullet>();
        emitter = GetComponentInChildren<ParticleSystem>();

        forwardMuzzleFlashScript = forwardMuzzleFlash.GetComponent<MuzzleFlash>();
        upMuzzleFlashScript = upMuzzleFlash.GetComponent<MuzzleFlash>();
        downMuzzleFlashScript = downMuzzleFlash.GetComponent<MuzzleFlash>();

        coolTime = WeaponsConstants.MachinGunCoolTime;
        for (int i = 0; i < PoolItemsCount; i++)
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
        if (canShoot == true)
        {
            coolTimer = 0;
            var bullet = inactiveBullets.First();
            if (bullet != null)
            {
                inactiveBullets.RemoveAt(0);
                activeBullets.Add(bullet);

                var offset = BulletPositionOffset;
                var initialDirectionChanged = direction.x < 0;
                if (initialDirectionChanged)
                {
                    offset *= -1;
                }

                bullet.gameObject.transform.position = gameObject.transform.position;
                bullet.gameObject.transform.Translate(offset, 0, 0);
                bullet.Direction = direction;
                bullet.gameObject.SetActive(true);
                emitter.Play();

                forwardMuzzleFlashScript.Play();
                upMuzzleFlashScript.Play(initialDirectionChanged);
                downMuzzleFlashScript.Play(initialDirectionChanged);
            }
        }

    }
}

