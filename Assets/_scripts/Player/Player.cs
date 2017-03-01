using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int Health = 100;

    private DivideSprite spriteDivider;

    // Use this for initialization
    void Start ()
    {
        spriteDivider = GetComponent<DivideSprite>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "RotPlatform")
        {
            var platform = coll.gameObject.GetComponent<Platform>();
            this.Health -= platform.Damage;
            if(Health <= 0)
            {
                spriteDivider.Divide();
                this.gameObject.SetActive(false);
            } 
        }
    }

}
