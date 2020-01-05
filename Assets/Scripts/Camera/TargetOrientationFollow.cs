using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrientationFollow : MonoBehaviour
{
    //Paramètres de position et d'orientation de la caméra (Il faudra mettre ça en private ensuite)
    public Camera camera;
    public Vector3 position;
    public Vector3 rotation;
    public GameObject player;
    public float near;
    public float far;
    public float interpolationSpeed;
    public float rotationSpeed;

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
        interpolationSpeed = 3;
        //Set des paramètres de zoom
        maxZoom = 20f;
        minZoom = 550f;
        sensitivity = 10f;
        rotationSpeed = 2;
    }


    void Update()
    {
        //On update la position de la caméra si jamais on change les paramètres à un moment (ça ne devrait cependant jamais être le cas car ça passera en private)
        camera.transform.position = player.transform.position;
        var newRot = Quaternion.Euler(rotation);
        //Interpolation de quaternion pour rendre la rotation plus smooth
        camera.transform.rotation = Quaternion.Slerp(transform.rotation, newRot, interpolationSpeed * Time.deltaTime);
        camera.nearClipPlane = near;
        camera.farClipPlane = far;

        //Reset de la rotation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotation.y = 45;
        }

        //Changement de rotation Y via les flèches
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            rotation.y += rotationSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            rotation.y -= rotationSpeed;
        }


        //Dezoom
        if (Input.GetAxis("Mouse ScrollWheel") < 0) 
        {
            camera.orthographicSize += sensitivity;
        }
        //Zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        {
            camera.orthographicSize -= sensitivity;
        }
        //Limite de zoom minimum et zoom maximum
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, maxZoom, minZoom);




    }
}
