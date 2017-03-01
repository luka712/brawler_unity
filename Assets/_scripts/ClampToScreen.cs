using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampToScreen :  MonoBehaviour
{
    private float widthBound;
    private float heightBound;

    private void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        widthBound = Screen.width - renderer.sprite.bounds.size.x * .5f;
        heightBound = Screen.height - renderer.sprite.bounds.size.y * .5f;
    }

	private void Update ()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        x = Mathf.Clamp(x, 0, widthBound);
        y = Mathf.Clamp(y, 0,heightBound);

        this.transform.position = new Vector3(x, y, this.transform.position.z);
	}
}
