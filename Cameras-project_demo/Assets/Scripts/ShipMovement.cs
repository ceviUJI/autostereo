using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
    Vector3 target;
    Vector3 targetRot;
    Vector2 A, B, C, D;
    Camera mainCam;

    [SerializeField]
    private float m_Speed = 50f;              // The speed the ship moves forward.
    [SerializeField]
    private float m_Damping = 0.3f;


    private float distanceToShip;
    private float sweetSpot;
    private float advanceDistance;


    private Vector2 screenStop=new Vector3(0.8f,0.8f);

    private const float k_ExpDampingCoef = -5f;
    private const float k_BankingCoef = 30f;
    private float releaseTime;
    private float t;
    private int sense = 1;
    private Vector3 inputPos;
    public bool moveNormally=true;
    StereoscopicCamController camController;
    public bool moveConvergencePlane;

    [SerializeField]
    private GameObject Bullet;
    public GameObject rendererInitialPosition;
    public LineRenderer lineRend;
	private int ct = 0;



    // Use this for initialization
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camController=mainCam.GetComponent<StereoscopicCamController>();
        sweetSpot = camController.SWEETSPOTLOCATION;
        inputPos = new Vector3();
        InvokeRepeating("MoveShip", 2.5f, 3f);
        //InvokeRepeating("MoveshipForward", 2.5f, 3f);
        lineRend.enabled = false;
    }
	void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target,0.01f);
        
    }
    // Update is called once per frame
    private void MoveShip() {
        if (moveNormally)
        {
            inputPos = new Vector3(Random.Range(0f, 1f) * mainCam.pixelWidth, Random.Range(0f, 1f) * mainCam.pixelHeight, inputPos.z);
            sense *= -1;
            
        }
    }
    private void MoveshipForward()
    {
        StopAllCoroutines();

    }
    void Update()
    {
        t = m_Damping * (1f - Mathf.Exp(k_ExpDampingCoef * Time.deltaTime/Time.timeScale));

        if (!moveNormally)
        {
            inputPos = Input.mousePosition;
        }

        //inputPos = Input.mousePosition;
        pointWithinVisibility(ref inputPos);

        Ray ray = mainCam.ScreenPointToRay(inputPos);
        distanceToShip = Vector3.Distance(mainCam.transform.position, this.transform.position);
		//Debug.Log (distanceToShip);

        target = ray.GetPoint(sweetSpot);

        if (moveConvergencePlane)
        {
            if (sense > 0)
            {
                camController.changeSweetSpotLocationDirectly(Mathf.Lerp(camController.Current_Sweetspot,
                        camController.SWEETSPOTLOCATION + 8, t));
            }
            else
            {
                camController.changeSweetSpotLocationDirectly(Mathf.Lerp(camController.Current_Sweetspot,
                        camController.SWEETSPOTLOCATION, t));
            }
            sweetSpot = camController.Current_Sweetspot;
        }
        else
        {
            if (sense > 0)
            {
                target = ray.GetPoint(Mathf.Lerp(this.transform.position.z,
                        camController.SWEETSPOTLOCATION +8, t*20 )); 
            }
            else
            {
                target = ray.GetPoint(Mathf.Lerp(this.transform.position.z,
                        camController.SWEETSPOTLOCATION , t*20));
            }

        }
      



        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveshipForward();

        }

        if (Input.GetMouseButton(0))
        {
            lineRend.enabled = true;
            lineRend.SetPosition(0, rendererInitialPosition.transform.position);
            lineRend.SetPosition(1, this.transform.forward*1000);
        }
        else
        {
            lineRend.enabled = false;
        }
        moveShip();
        Debug.DrawLine(mainCam.transform.position, target);
        //Debug.Log(target);

    }

    private void moveShip()
    {
       
        Vector3 newPos = Vector3.Lerp(this.transform.position, target,t);
        this.transform.position = newPos;
        Vector3 dist = this.transform.position - target;
		ct = ct + 1;
		//Debug.Log (ct);
		//Debug.Log (distanceToShip);

        // Base the target markers pitch (x rotation) on the distance in the y axis and it's roll (z rotation) on the distance in the x axis.
        targetRot = new Vector3(dist.y, 0f, dist.x*2) *k_BankingCoef;

        // Make the flyer bank towards the marker.
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation,  Quaternion.Euler(targetRot),t);

    }
    private void shoot() {
      /*  Bullet =GameObject.Instantiate(Bullet) as GameObject;
        var behav =Bullet.AddComponent<BulletBehaviour>();
        Bullet.transform.position=this.transform.position;
        behav.endPos = this.GetComponentInChildren<Transform>().transform.forward;*/
        
    
    }
    IEnumerator advance(float from,float to,float time,float sense   )
    {
        
        for (float t = 0.0f;  Mathf.Abs(t) < 1.0f; t += Time.deltaTime / time )
        {
            Debug.Log(t);
            if (sense > 0)
                camController.changeSweetSpotLocationDirectly(Mathf.Lerp(from, to, t));
            else {
                camController.changeSweetSpotLocationDirectly(Mathf.Lerp(to, from, t));
            }
            yield return null;
        }
        //float t =m_Damping * (1f - Mathf.Exp(k_ExpDampingCoef * Time.deltaTime));
    }
    private void pointWithinVisibility(ref Vector3 P) {
        if (P.x < Screen.width * (1 - screenStop.x))
        {
            P.x = Screen.width * (1 - screenStop.x);
        }
        else if (P.x > Screen.width * screenStop.x)
        {
            P.x = Screen.width * screenStop.x;
        }

        if (P.y < Screen.height * (1 - screenStop.y))
        {
            P.y = Screen.height * (1 - screenStop.y);
        }
        else if (P.y > Screen.height * screenStop.y)
        {
            P.y = Screen.height * screenStop.y;
        }
        
    }
}
