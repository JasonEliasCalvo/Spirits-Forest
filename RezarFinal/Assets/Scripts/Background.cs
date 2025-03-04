using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - cameraTransform.position;
    }

    void Update()
    {
        transform.position = new Vector3(cameraTransform.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
