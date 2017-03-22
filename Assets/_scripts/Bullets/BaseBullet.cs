using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    /// <summary>
    /// Gets or sets damage done by bullet.
    /// </summary>
    public virtual int Damage { get; protected set; }

    [SerializeField]
    private PlayerIndex playerIndex;
    public PlayerIndex PlayerIndex { get { return playerIndex; } }

    protected Vector2 direction;
    /// <summary>
    ///  Gets or sets direction.
    ///  When set direction is normalized.
    /// </summary>
    public Vector2 Direction
    {
        get { return direction; }
        set
        {
            direction = value;
            direction.Normalize();
        }
    }

    /// <summary>
    /// Gets or sets travel speed.
    /// </summary>
    public float Speed { get; protected set; }
}
