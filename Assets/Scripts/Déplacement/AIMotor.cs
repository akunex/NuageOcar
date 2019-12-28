using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIMotor : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    private Vector3 targetPosition;
    private Vector3 inputTargetPosition;
    public GameObject NPC;

    private bool isMoving;
    private bool isAttacking;

    public AnimationClip run;
    public AnimationClip idle;
    private float metersPoint;
    private int seconds;
    private float timer;

    private RaycastHit hit;
    private Ray ray;


    void Start()
    {
        isMoving = false;
        timer = 0.0f;
    }

    void Update()
    {
        if (isMoving)
        {
            this.GetComponent<Animation>().Play("run");
            metersPoint = CalculatePathLength(inputTargetPosition) / 20;
            if (metersPoint < 0.2)
            {
                timer = UnityEngine.Random.Range(3.0f, 20.0f);
                isMoving = false;
            }
        }
        else
        {
            this.GetComponent<Animation>().Play("idle");
            timer -= Time.deltaTime;
            seconds = Convert.ToInt32(timer % 60);
            if (seconds < 0)
            {
                MoveNPC();
            }
        }
    }

    void MoveNPC()
    {
        var playerObject = NPC;
        float spawnZ = UnityEngine.Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = UnityEngine.Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        float spawnY = (float)-2.15625;
        Vector3 vec = new Vector3(spawnX, spawnY, spawnZ);
        ray = new Ray(playerObject.transform.position, vec);

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

    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }

    private IEnumerator WaitForMoving()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(3.0f, 20.0f));
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