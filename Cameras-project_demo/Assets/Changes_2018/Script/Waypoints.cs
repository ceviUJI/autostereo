using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour {

    public bool gizmosActivate;
    public Transform pathHolder;

    public float speed;
    public float waitTime;

    private int indexway = 0;

    public float timer = 0f;

    public Text timerText;
    public Text warningText;


    private ShipMovementGame shipManual;

	// Use this for initialization
	void Start () {

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            
        }

        StartCoroutine(FollowPath(waypoints));
        shipManual = GetComponent<ShipMovementGame>();
        shipManual.enabled = false;
        warningText.enabled = false;
    }
	
    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[indexway];

        int targetWaypointIndex = indexway + 1;

        Vector3 targetWaypoint = waypoints[targetWaypointIndex];

        while(true)
        {
            //transform.LookAt(targetWaypoint);
           

            Quaternion look = Quaternion.LookRotation(targetWaypoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex++;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
            }
            if(targetWaypointIndex > waypoints.Length)
            {
                yield return null;
            }
            yield return null;
        }
       
        
    }

    private void OnDrawGizmos()
    {
        if (gizmosActivate)
        {
            Vector3 startPosition = pathHolder.GetChild(0).position;
            Vector3 previousPosition = startPosition;
            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, 1f);
                Gizmos.DrawLine(previousPosition, waypoint.position);
                previousPosition = waypoint.position;
            }
            //Gizmos.DrawLine(previousPosition, startPosition);
        }

    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        timerText.text = "TIME: " + (int) timer;

        if(timer >= 18f)
        {
            warningText.enabled = true;
            warningText.transform.localScale = Vector3.Lerp(warningText.transform.localScale, new Vector3(3, 3, 3), Time.deltaTime);
        }

        if (timer >= 20f && timer < 23f)
        {
            Debug.Log("Se termina el automatico pasamos al manual");
            
            StopAllCoroutines();
           
           
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime);


        }
        else if(timer > 23f)
        {
            this.enabled = false;
            shipManual.enabled = true;
            warningText.enabled = false;
        }

	}
}
