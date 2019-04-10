using UnityEngine;
using System.Collections;

public class BasicBehaviour : MonoBehaviour {

    private float speed = 5;

    protected float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    protected GameController controller;
    protected Material mat;
    float spawnTime;
    float arriveTime;

    protected GameObject camera;

    Vector3 rotationangle;
    Vector3 spawnPos;
    protected Vector3 arrivePosition;

    public static AnimationCurve trajectory;
    

    // Use this for initialization
    protected virtual void configureParams()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera c = camera.GetComponent<Camera>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            
        arrivePosition = this.transform.position;
        arrivePosition.z = -20;
        arriveTime = (this.transform.position - arrivePosition).magnitude / speed;

        spawnTime = Time.time;
        spawnPos = this.transform.position;
        rotationangle = Vector3.right * Random.Range(0f, 1f) + Vector3.up * Random.Range(0f, 1f) + Vector3.forward * Random.Range(0f, 1f);

    }

   
    public virtual void restartMovement() {
       
        arriveTime = (this.transform.position - arrivePosition).magnitude/speed;
        spawnPos = this.transform.position;
        spawnTime = Time.time;

    }

    public virtual void move() {
        this.transform.position = Vector3.Lerp(spawnPos, arrivePosition, trajectory.Evaluate((Time.time - spawnTime) / arriveTime));
        if ((Time.time - spawnTime) / arriveTime > 1)
        {
            this.gameObject.SetActive( false);
        }
       
    }

}
