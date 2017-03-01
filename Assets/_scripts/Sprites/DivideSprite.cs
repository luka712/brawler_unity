using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideSprite : MonoBehaviour
{
    [SerializeField]
    private static Vector2 pass = new Vector2(4, 4);


    private Sprite sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        Debug.Log(sprite);
    }

    public IEnumerable<GameObject> Divide()
    {
        var totalSpriteSize = new Vector2(sprite.texture.width, sprite.texture.height);

        var xPass = totalSpriteSize.x / pass.x;
        var yPass = totalSpriteSize.y / pass.y;

        for (float x = 0; x < totalSpriteSize.y; x += xPass)
        {
            for (float y = 0; y < totalSpriteSize.y; y += yPass)
            {
                var gameObject = CreateGameObject(
                    Sprite.Create(sprite.texture, new Rect(x, y, xPass, yPass), Vector2.zero, 1));

                float xPos = -(xPass * pass.x * .5f) + x + this.transform.position.x;
                float yPos = -(yPass * pass.y * .5f) + y + this.transform.position.y;
                gameObject.transform.position = new Vector3(xPos, yPos, this.transform.position.z);
                yield return gameObject;
            }
        }
    }

    private GameObject CreateGameObject(Sprite sprite)
    {
        var gameObject = new GameObject();
        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        return gameObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            var spriteObjects = Divide();
            foreach (var spriteObj in spriteObjects)
            {
                var rb = spriteObj.AddComponent<Rigidbody2D>();
                rb.AddForce(new Vector2((float)Random.Range(-5000, 5000), (float)Random.Range(-5000, 5000)));
            }
        }
    }
}
