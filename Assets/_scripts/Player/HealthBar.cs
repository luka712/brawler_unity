using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Health bar.
/// </summary>
public class HealthBar : MonoBehaviour
{
    private Player player;
    private Vector3 initialScale;

    /// <summary>
    /// Unity start.
    /// </summary>
    private void Start()
    {
        initialScale = gameObject.transform.localScale;
        player = GetComponentInParent<Player>();
        player.OnDamageApplied += (health) =>
        {
            gameObject.transform.localScale = initialScale * health * 0.01f;
        };
    }
}
