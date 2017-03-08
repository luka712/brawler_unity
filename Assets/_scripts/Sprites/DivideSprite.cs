using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideSprite : MonoBehaviour
{
    [SerializeField]
    private Vector2 pass = new Vector2(3, 3);

    private SpriteRenderer spriteRenderer;
    private IList<DividedSprite> gameObjects = new List<DividedSprite>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < pass.x * pass.y; i++)
        {
            gameObjects.Add(new DividedSprite(this.gameObject));
        }
    }

    private void Update()
    {
        for(int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].Update();
        }
    }

    private IEnumerable<DividedSprite> GetGameObjectsWithSprite()
    {
        var totalSpriteSize = new Vector2(spriteRenderer.sprite.texture.width,
            spriteRenderer.sprite.texture.height);

        var xPass = totalSpriteSize.x / pass.x;
        var yPass = totalSpriteSize.y / pass.y;

        int i = 0;
        for (float x = 0; x < totalSpriteSize.y; x += xPass)
        {
            for (float y = 0; y < totalSpriteSize.y; y += yPass)
            {
                var dividedSprite = gameObjects[i++];
                float xPos = -(xPass * pass.x * .5f) + x;
                float yPos = -(yPass * pass.y * .5f) + y;
                dividedSprite.CreateSprite(this.spriteRenderer, 
                    new Rect(x, y, pass.x, pass.y),
                    new Vector2(xPos, yPos));
                yield return dividedSprite;
            }
        }
    }

    public void Divide(Vector2 direction, float minForce = 1 , float maxForce = 5)
    {
        var spriteObjects = GetGameObjectsWithSprite();
        foreach (var spriteObj in spriteObjects)
        {
            spriteObj.AddForce(direction, minForce, maxForce);
        }
    }

}
