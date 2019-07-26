using UnityEngine;
using UnityEngine.AI;

public class CharacterMotor : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    private Vector3 targetPosition;

    private bool isMoving;

    public AnimationClip run;
    public AnimationClip idle;

    void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                targetPosition = hit.point;
            }
            
        }
        // Il y a peut être une meilleur solution ?
        Debug.Log(Vector3.Distance(transform.position, targetPosition));
        Debug.DrawLine(transform.position, targetPosition, Color.red);
        if (Vector3.Distance(transform.position, targetPosition) < 1)
        {
            isMoving = false;
        }
        if (isMoving)
        {
            this.GetComponent<Animation>().Play("run");
        }
        else
        {
            this.GetComponent<Animation>().Play("idle");
        }
    }
}