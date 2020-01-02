using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpellOne : MonoBehaviour
{

    public Button spell;
    public Rigidbody ball;
    public Transform target;
    public GameObject targetGO;
    public GameObject instanciatedGO;
    public Material mat;
    public float h = 25;
    public float gravity = -18;

    public bool debugPath;

    public LayerMask movementMask;

    public bool respectedDistance;
    public bool useSpell;

    private void Awake()
    {
        spell = GetComponent<Button>();
    }
    void Start()
    {
        ball.useGravity = false;
        target = null;
        respectedDistance = false;
        useSpell = false;
        spell = GameObject.Find("Arrow").GetComponent<Button>();
        
    }

    void Update()
    {

        if(spell !=null)
            spell.onClick.AddListener(OnSpellButton);
        if (useSpell)
            DrawSpell();
       
        
    }

   

    public void DrawSpell()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, movementMask))
        {
            targetGO = Instantiate(instanciatedGO = new GameObject(), hit.point, Quaternion.identity);
            target = targetGO.transform;
            targetGO.name = "Trait";
            instanciatedGO.name = "Trait";
            Debug.Log(Vector3.Distance(hit.point, ball.gameObject.transform.position));
            if (Vector3.Distance(hit.point, ball.gameObject.transform.position) < 250)
            {
                respectedDistance = true;
            }
            else
            {
                respectedDistance = false;
            }
            Destroy(targetGO);
            Destroy(instanciatedGO);
        }

        if (respectedDistance)
            DrawPath();
    }

    public void OnSpellButton()
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
        GameObject myLine = new GameObject();
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