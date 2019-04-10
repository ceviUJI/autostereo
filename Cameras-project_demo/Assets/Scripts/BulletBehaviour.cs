using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 endPos;
	// Use this for initialization
	void Start () {
       startPos= this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position +=  endPos * 2f * Time.deltaTime;
	}
}
