using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrientationFollow : MonoBehaviour
{
    public Vector3 position;
    public Vector3 rotation;
    public float near;
    public float far;
    public Camera camera;


    void Update()
    {
        float minFov = 15f;
        float maxFov = 90f;
        float sensitivity = 10f;
        camera.transform.position = position;
        camera.transform.rotation = Quaternion.Euler(rotation);
        camera.nearClipPlane = near;
        camera.farClipPlane = far;

    }
}
