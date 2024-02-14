using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraTransform.position;
    }
}
