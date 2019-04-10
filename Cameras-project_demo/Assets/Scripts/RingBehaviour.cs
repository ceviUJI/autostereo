using UnityEngine;
using System.Collections;

public class RingBehaviour : BasicBehaviour {
    Transform[] parts;
    float multiplier;
	// Use this for initialization
	void Start () {
        base.configureParams();
        parts = this.GetComponentsInChildren<Transform>();
        base.arrivePosition = base.camera.transform.position - Vector3.forward * 10;
        multiplier = Random.Range(-1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        base.move();


        for (int i = 0; i < parts.Length; i++) {
            if (i % 2 == 0)
            {
                parts[i].rotation = Quaternion.Euler(Vector3.forward * Mathf.Sin(Time.time*multiplier/Time.timeScale) * 360);
            }
            else {
                parts[i].rotation = Quaternion.Euler(Vector3.forward * Mathf.Cos(Time.time/Time.timeScale) * 360);
            }
        }

	}
}
