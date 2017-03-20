using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    private int damage;

    protected float coolTimer;
    protected float coolTime;
    protected bool canShoot;

    protected List<BaseBullet> inactiveBullets;
    protected List<BaseBullet> activeBullets;

    private Vector2 offset = Vector2.one * 100;

    /// <summary>
    /// Gets weapon damage
    /// </summary>
    public int Damage { get { return damage; } }

    /// <summary>
    /// Unity start.
    /// </summary>
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Unity update
    /// </summary>
    protected void Update()
    {
        coolTimer += Time.deltaTime;
        if (coolTimer > coolTime)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            if(activeBullets[i] == null)
            {
                continue;
            }

            var pos = activeBullets[i].gameObject.transform.position;
            if (pos.x > Screen.width + offset.x
                || pos.x < -offset.x
                || pos.y < -offset.y
                || pos.y > Screen.height + offset.y)
            {
                inactiveBullets.Add(activeBullets[i]);
                activeBullets[i].gameObject.SetActive(false);
                activeBullets.RemoveAt(i);
            }
    }
}

/// <summary>
/// Call from impelementation class for shooting.
/// </summary>
public abstract void Shoot(Vector2 direction);
}
