using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{

    public float speed = 10;
    public float minDistance = 1;
    private Vector3 targetPosition;
    private Vector3 targetOrientation;
    private bool isMoving;

    const int LEFT_MOUSE_BUTTON = 0;

    public AnimationClip run;
    public AnimationClip idle;

    void Start() {
        targetPosition = transform.position;
        isMoving = false;
    }

   
    void Update() {
        if (Input.GetMouseButton(LEFT_MOUSE_BUTTON))
            SetTargetPosition();
        if (isMoving)
        {
            MovePlayer();
            GetComponent<Animation>().Play(run.name);
        } 
        else
        {
            GetComponent<Animation>().Play(idle.name);
        }
            
    }

    void SetTargetPosition() {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
            targetPosition = ray.GetPoint(point);

        isMoving = true;
    }

    void MovePlayer() {
        /*
        Orientation du joueur
        On place la même hauteur que le playerbody pour qu'il ne regarde pas vers le bas afin que le corps reste droit, car le point sur lequel on clique est haut sol
        Le player va regarder le sol comme un con.. et ne plus être droit
        */
        targetOrientation = targetPosition;
        targetOrientation.y = transform.position.y;
        transform.LookAt(targetOrientation);
        // Déplacement du joueur
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // On ne prend pas en compte le y pour éviter qu'il cours à l'infinie sur du relief. Quand on clique sur du relief, le point va en dessous.
        // Il faut trouver un moyen que ça ne le fasse pas
        if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z || Vector3.Distance(transform.position, targetPosition) < minDistance)
            isMoving = false;

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }
}
