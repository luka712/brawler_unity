using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvicibleTimeAnimation : MonoBehaviour
{
    /// <summary>
    /// Playtime speed.
    /// </summary>
    [SerializeField]
    private float playSpeed = 1.5f;

    /// <summary>
    /// Number of time to play animation.
    /// </summary>
    [SerializeField]
    private int playTimes = 3;
    private int playedTimes = 0;

    /// <summary>
    /// Public bool which indicated animation playing state.
    /// </summary>
    [HideInInspector]
    public bool IsPlaying { get; private set; }

    /// <summary>
    /// Animation finsihed event handler.
    /// </summary>
    public event Action OnAnimationFinished;

    private SpriteRenderer rend;
    private float alphaColor;
    private bool alphaGoingUp;

    #region Unity Methods

    /// <summary>
    /// START
    /// </summary>
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        alphaColor = 1f;
        alphaGoingUp = false;
    }

    /// <summary>
    /// UPDATE
    /// </summary>
    private void Update()
    {
        if (IsPlaying)
        {
            if(alphaGoingUp)
            {
                alphaColor += Time.deltaTime * playSpeed;
                if(alphaColor >= 1f)
                {
                    alphaGoingUp = false;
                    if(++playedTimes >= playTimes)
                    {
                        if(OnAnimationFinished != null)
                        {
                            OnAnimationFinished();
                        }
                        IsPlaying = false;
                        playedTimes = 0;
                    }
                }
            }
            else
            {
                alphaColor -= Time.deltaTime * playSpeed;
                if(alphaColor <= 0f)
                {
                    alphaGoingUp = true;
                }
            }
            alphaColor = Mathf.Clamp(alphaColor, 0f, 1f);
            SetSpriteAlphaColor(alphaColor);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Start playing fading animations.
    /// </summary>
    public void Play()
    {
        IsPlaying = true;
        playedTimes = 0;
        rend.color = rend.color.SetAlpha(0f);
    }

    /// <summary>
    /// Sets sprite color.
    /// </summary>
    private void SetSpriteAlphaColor(float alpha)
    {
        rend.color = rend.color.SetAlpha(alpha);
    }

    #endregion


}
