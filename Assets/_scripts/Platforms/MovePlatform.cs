using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    [SerializeField]
    private Vector2 startPoint;
    [SerializeField]
    private Vector2 endPoint;
    [SerializeField]
    private float speed;

    private Vector2 direction;

    private bool towardsEnd = true;
    private bool towardsStart = false;

    // Use this for initialization
    private void Start()
    {
        this.transform.position = new Vector3(startPoint.x, startPoint.y, this.transform.position.z);
        direction = endPoint - startPoint;
        direction.Normalize();
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = direction * speed * Time.deltaTime;
        this.transform.position += direction.ToVector3();
        if(towardsEnd && Vector2.Distance(this.transform.position.ToVector2(), endPoint) < 1f)
        {
            towardsEnd = false;
            towardsStart = true;
            direction *= -1;
        }
        else if(towardsStart && Vector2.Distance(this.transform.position.ToVector2(), startPoint) < 1f)
        {
            towardsEnd = true;
            towardsStart = false;
            direction *= -1;
        }
    }
}
