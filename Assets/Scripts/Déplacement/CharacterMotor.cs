using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterMotor : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    private Vector3 targetPosition;
    private Vector3 inputTargetPosition;
    public Text displayMeters;
    public Image image;

    private bool isMoving;

    public AnimationClip run;
    public AnimationClip idle;

    void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        //if (agent.tag == "Player")
        //{
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out hit))
            {
                tag = hit.transform.gameObject.tag;
                if (tag != "Not Clickable")
                {
                    isMoving = true;
                    agent.SetDestination(hit.point);
                    inputTargetPosition = hit.point;
                }
            }


        }
        if (Vector3.Distance(transform.position, inputTargetPosition) < 1)
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
        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;

            image.enabled = true;
            displayMeters.enabled = true;
            ShowPathNavMesh();
            float meters = CalculatePathLength(targetPosition) / 20;
            displayMeters.text = meters.ToString("0") + " mètres";

        }
        //    }
        //    else if(agent.tag == "AI")
        //    {
        //        MoveNPC();
        //    }
    }

    void MoveNPC()
    {

    }

    void ShowPathNavMesh()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.cyan };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.cyan, Color.cyan);
        }

        NavMeshPath navPath = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPosition, navPath);

        var path = nav.path;

        line.SetVertexCount(navPath.corners.Length);

        for (int i = 0; i < navPath.corners.Length; i++)
        {
            line.SetPosition(i, navPath.corners[i]);
        }

    }


    float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}