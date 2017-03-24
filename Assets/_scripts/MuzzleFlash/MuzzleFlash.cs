using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation that fadoes out overtime
/// </summary>
public class MuzzleFlash : MonoBehaviour
{

    [SerializeField]
    private float fadeOutSpeed = 3f;

    /// <summary>
    /// X range.
    /// </summary>
    public Vector2 XRange = new Vector2(.6f, 1f);

    /// <summary>
    /// Y range
    /// </summary>
    public Vector2 YRange = new Vector2(.6f, 1f);

    /// <summary>
    /// Rotation offset.
    /// </summary>
    public Vector2 MinAndMaxRotation = new Vector2(-10f, 10f); 

    private SpriteRenderer renderer;
    private bool play;
    private Color initialColor;
    private Quaternion initialRotation;

    /// <summary>
    /// Unity start
    /// </summary>
	private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        initialColor = renderer.color;
        initialRotation = this.transform.rotation;
    }

    /// <summary>
    /// Unity update
    /// </summary>
    private void Update()
    {
        if (play)
        {
            initialColor.a -= Time.deltaTime * fadeOutSpeed;
            renderer.color = initialColor;
            if (initialColor.a <= 0f)
            {
                play = false;
            }
        }
    }

    /// <summary>
    /// Plays animation.
    /// </summary>
    public void Play(bool applyDirectionChange = false)
    {
        this.transform.localScale
            = new Vector3(Random.Range(XRange.x, XRange.y), Random.Range(YRange.x, YRange.y), 1f);
        initialColor.a = 1f;
        renderer.color = initialColor;

        this.transform.rotation = initialRotation;

        if(applyDirectionChange)
        {
            this.transform.Rotate(0, 0, 180);
        }

        this.transform.Rotate(0, 0, Random.Range(MinAndMaxRotation.x, MinAndMaxRotation.y));

        play = true;
    }
}
