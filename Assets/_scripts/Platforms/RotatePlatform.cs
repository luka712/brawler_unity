using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50;

    private void Update()
    {
        this.transform.Rotate(Vector3.forward * rotationSpeed);
    }
}
