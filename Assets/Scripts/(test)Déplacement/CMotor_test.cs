using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class CMotor_test : MonoBehaviour
{ 
    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;

    public Interactable focus;

    public bool canMove;

    void Start()
    {
        canMove = true;
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (!canMove)
        {
            motor.agent.ResetPath();
            motor.agent.isStopped = true;
        }
        else
        {
            motor.agent.isStopped = false;
            
        }
        //Permet de pas se déplacer si on clique sur l'ui
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Déplacement clique gauche
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, movementMask))
            {
                motor.MoveToPoint(hit.point);
                this.transform.LookAt(hit.point);
                RemoveFocus();
            }
        }

        //Interaction clique droite
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Pour savoir si on clique sur un objet avec lequel on peut interagir
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        

        }
    }

    //On se focus sur l'objet et donc on va le suivre et interagir avec lui
    void SetFocus (Interactable newFocus)
    {
        //Si on focus un nouvel objet, on défocus l'ancien
        if (newFocus != focus)
        {
            //Evite les bug ou le focus est null
            if (focus != null)
            {
                focus.OnDefocused();
            }
            
            //Le focus devient l'objet sur lequel on a clique droite et on se dirige dessus
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
        
    }

    //On arrete de focus l'objet, on ne le suit plus et on interagit plus avec lui
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
