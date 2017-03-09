using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedSprite
{
    private GameObject _this;
    private GameObject parent;
    private SpriteRenderer rend;
    private BoxCollider2D coll;
    private Rigidbody2D rigidBody;

    // added for fine tunning details.
    private const float ForceMultiplier = 100;

    // gravity
    private const float Gravity = 5f;
    private const float FadeOutSpeed = 20f;
    private float lifeTime = 0;
    private bool isActive = false;

    /// <summary>
    /// Sets collision layer.
    /// </summary>
    [SerializeField]
    private string collisionLayer = "Tiles";

    #region Constructors

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parent">Parent object</param>
    public DividedSprite(GameObject parent)
    {
        // Using this approach since approach with MonoBehaviour wasn't working for me.
        // It probably works, but I already got this working this way so... why not
        _this = new GameObject();
        this.parent = parent;
        rend = _this.AddComponent<SpriteRenderer>();
        coll = _this.AddComponent<BoxCollider2D>();
        rigidBody = _this.AddComponent<Rigidbody2D>();
        rigidBody.gravityScale = Gravity;
        rigidBody.gameObject.layer = LayerMask.NameToLayer(collisionLayer);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Update
    /// note: This is not Unity Update method, since this class doesn't inherit MonoBehaviour
    /// Should be called in Update of prefab object that has this script. 
    /// DivideSprites are independent objects created at runtime.
    /// </summary>
    public void Update()
    {
        if (isActive && lifeTime > 0)
        {
            // multiplication is faster then division. So 0.01f.
            lifeTime -= Time.deltaTime * FadeOutSpeed * 0.01f;
            rend.color = rend.color.SetAlpha(lifeTime);
            if (lifeTime <= 0)
            {
                _this.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Create sprite.
    /// </summary>
    /// <param name="parentRenderer">Parent renderer. This objects uses same sprite and color as parent.</param>
    /// <param name="sourceRect">Part of sprite to render.</param>
    /// <param name="offset">Position offset.</param>
    public virtual void CreateSprite(SpriteRenderer parentRenderer, Rect sourceRect, Vector2 offset)
    {
        _this.SetActive(true);
        isActive = true;
        lifeTime = 1f;
        rend.sprite = Sprite.Create(parentRenderer.sprite.texture, sourceRect, Vector2.zero, 1);
        rend.color = parentRenderer.color;

        var parentPosition = parent.transform.position;
        _this.transform.position =
            new Vector3(parentPosition.x + offset.x, parentPosition.y + offset.y, parentPosition.z);

        // Set collision box.
        coll.offset = new Vector2(sourceRect.width * .5f, sourceRect.height * .5f);
        coll.size = new Vector2(sourceRect.width, sourceRect.height);
    }

    /// <summary>
    /// Applies randomized force to divided sprite.
    /// </summary>
    /// <param name="direction">Direction.</param>
    /// <param name="minForce">Multiplier min force.</param>
    /// <param name="maxForce">Multiplier max force.</param>
    public virtual void AddForce(Vector2 direction, float minForce, float maxForce)
    {
        rigidBody.AddForce(new Vector2(direction.x * Random.Range(minForce, maxForce),
            direction.y * Random.Range(minForce, maxForce)) * ForceMultiplier);
    }

    #endregion
}
