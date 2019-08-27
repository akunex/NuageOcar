using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;

    void Start()
    {
        //On prend le navmesh de l'agent en question
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Si il existe une target alors on lui fait face et on se dirige dessus (donc on la suit même si elle bouge tant qu'on la focus)
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    //Permet de se déplacer simplement avec le clique gauche
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    //Suivre une cible (on définie une nouvelle target pour le Update() et on définie la distance d'arret devant celle ci)
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius* .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    //Ne plus suivre une cible (on remet la target à null)
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }


    //Permet de rester face à l'objet même lorsque l'on est dans sa hitbox au cas ou l'objet se déplace autour de nous (comme on reste dans son radius on bouge pas mais on a besoin de se tourner
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position);
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);
    }
}
