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
        RaycastHit hit;
        string tag = "";
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                tag = hit.transform.gameObject.tag;
                if (tag != "Not Clickable")
                {

                    isMoving = true;
                    agent.SetDestination(hit.point);
                    targetPosition = hit.point;
                }
            }

            
        }
        if (tag != "Not Clickable")
        {
            // Il y a peut être une meilleur solution ?
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
}