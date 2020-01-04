using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SpellOne : MonoBehaviour
{

    public Button spell;
    public Rigidbody ball;
    public Transform target;
    public GameObject targetGO;
    public GameObject arrowGO;
    public GameObject arrowGOClone;
    public Material mat;
    public float h = 25;
    public float gravity = -18;

    public bool debugPath;

    public LayerMask movementMask;

    public bool respectedDistance;
    public bool useSpell;

    CMotor_test playerMovement;

    private void Awake()
    {
        spell = GetComponent<Button>();
    }
    void Start()
    {
        ball.useGravity = false;
        Instantiate(targetGO = new GameObject("Trait"), new Vector3(0,0,0), Quaternion.identity);
        targetGO.name = "Trait";
        target = null;
        respectedDistance = false;
        useSpell = false;
        spell = GameObject.Find("Arrow").GetComponent<Button>();


    }

    void Update()
    {
      

        if (useSpell)
        {
            if (DrawSpell() && Input.GetMouseButtonDown(0))
            {
                arrowGOClone = Instantiate(arrowGO, new Vector3(this.transform.position.x, this.transform.position.y + 40, this.transform.position.z), Quaternion.identity);
                ball = arrowGOClone.GetComponent<Rigidbody>();
                Launch();
                ball = this.GetComponent<Rigidbody>();
                useSpell = !useSpell;
            }
            if (DrawSpell() && Input.GetMouseButtonDown(1))
            {
                useSpell = !useSpell;
            }


        }

       
        
    }

   

    public bool DrawSpell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, movementMask))
        {
 
            targetGO.transform.position = hit.point;
            target = targetGO.transform;
 

            if (Vector3.Distance(hit.point, ball.gameObject.transform.position) < 250)
            {
                respectedDistance = true;
            }
            else
            {
                respectedDistance = false;
            }

        }

        if (respectedDistance)
        {
            DrawPath();
            return true;
        }
        else
            return false;
    }

    public void ActivateSpell()
    {
        useSpell = !useSpell;
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        if(target == null)
        {
            targetGO = new GameObject();
            target = targetGO.transform;
            target.position = new Vector3(0, 0, 0);
        }
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            DrawLine(previousDrawPoint, drawPoint, Color.green, 0.1f);
            
            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }



    void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        GameObject myLine = new GameObject("Trait DrawLine");
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = mat;
        lr.startColor = color;
        lr.endColor = color;
        lr.SetWidth(5f, 5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }



    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }


}