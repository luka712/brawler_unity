using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    /// <summary>
    /// Gets or sets pickup item type.
    /// </summary>
    [SerializeField]
    private PickUpItemType type;

    /// <summary>
    /// Returns pick up type.
    /// </summary>
    public PickUpItemType  Type { get { return type; } }

    private Rigidbody2D rigBody;

    protected void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        rigBody.gravityScale = Constants.Gravity;
    }
           
}
