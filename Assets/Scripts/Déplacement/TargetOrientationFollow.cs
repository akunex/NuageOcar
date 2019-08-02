using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrientationFollow : MonoBehaviour
{
    //Paramètres de position et d'orientation de la caméra (Il faudra mettre ça en private ensuite)
    public Camera camera;
    public Vector3 position;
    public Vector3 rotation;
    public float near;
    public float far;

    //Paramètres de zoom de la caméra
    public float minZoom;
    public float maxZoom;
    public float sensitivity;

    void Start()
    {
        //Set de la position de la caméra
        position.Set(0,0,0);
        rotation.Set(30, 45, 0);
        near = -1000;
        far = 1000;
        //Set des paramètres de zoom
        maxZoom = 15f;
        minZoom = 150f;
        sensitivity = 10f;
    }


    void Update()
    {
        //On update la position de la caméra si jamais on change les paramètres à un moment (ça ne devrait cependant jamais être le cas car ça passera en private)
        camera.transform.position = position;
        camera.transform.rotation = Quaternion.Euler(rotation);
        camera.nearClipPlane = near;
        camera.farClipPlane = far;

        //Dezoom
        if (Input.GetAxis("Mouse ScrollWheel") < 0) 
        {
            Camera.main.orthographicSize += sensitivity;
        }
        //Zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        {
            Camera.main.orthographicSize -= sensitivity;
        }
        //Limite de zoom minimum et zoom maximum
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, maxZoom, minZoom);




    }
}
