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

    private const float ForceMultiplier = 100;
    private const float Gravity = 5f;
    private const float LifeStart = 100;
    private float amountTilEnd = 0;
    private bool isActive = false;

    public DividedSprite(GameObject parent)
    {
        _this = new GameObject();
        this.parent = parent;
        rend = _this.AddComponent<SpriteRenderer>();
        coll = _this.AddComponent<BoxCollider2D>();
        rigidBody = _this.AddComponent<Rigidbody2D>();
        rigidBody.gravityScale = Gravity;
        rigidBody.gameObject.layer = LayerMask.NameToLayer("Tiles");
    }

    public void Update()
    {
        if (isActive && amountTilEnd > 0)
        {
            amountTilEnd -= Time.deltaTime * 10f;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, amountTilEnd);
            if (amountTilEnd <= 0)
            {
                _this.gameObject.SetActive(false);
            }
        }
    }

    public virtual void CreateSprite(SpriteRenderer parentRenderer, Rect sourceRect, Vector2 offset)
    {
        _this.SetActive(true);
        isActive = true;
        amountTilEnd = LifeStart;
        rend.sprite = Sprite.Create(parentRenderer.sprite.texture, sourceRect, Vector2.zero, 1);
        rend.color = parentRenderer.color;

        var parentPosition = parent.transform.position;
        _this.transform.position =
            new Vector3(parentPosition.x + offset.x, parentPosition.y + offset.y, parentPosition.z);

        coll.offset = new Vector2(sourceRect.width * .5f, sourceRect.height * .5f);
        coll.size = new Vector2(sourceRect.width, sourceRect.height);
    }

    public virtual void CreateSprite(SpriteRenderer parentRenderer, float x, float y, float width, float height, float xOffset, float yOffset)
    {
        CreateSprite(parentRenderer, new Rect(x, y, width, height), new Vector2(xOffset, yOffset));
    }

    public virtual void CreateSprite(SpriteRenderer parentRenderer, Rect sourceRect, float xOffset, float yOffset)
    {
        CreateSprite(parentRenderer, sourceRect, new Vector2(xOffset, yOffset));
    }

    public virtual void CreateSprite(SpriteRenderer parentRenderer, float x, float y, float width, float height, Vector2 offset)
    {
        CreateSprite(parentRenderer, new Rect(x, y, width, height), offset);
    }

    public virtual void AddForce(Vector2 direction, float minForce, float maxForce)
    {
        rigidBody.AddForce(new Vector2(direction.x * Random.Range(minForce, maxForce),
            direction.y * Random.Range(minForce, maxForce)) * ForceMultiplier);
    }
}
