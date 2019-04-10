using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class size : MonoBehaviour {

	//float width;
	//float height;
	//float tamano;
	Vector3 movement;  
	Rigidbody cubeRigidbody;  
	float factor = 0.01f;
	float depth = 0f;

	public Text Depth_C;
	

	void Awake()
	{
		//width = GetComponent<MeshRenderer> ().bounds.extents.x;
		//height = GetComponent<MeshRenderer>().bounds.extents.y; 

		//tamano = GetComponent<MeshRenderer> ().bounds.size.x;
		cubeRigidbody = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		//Debug.Log (width);
		//Debug.Log (tamano);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float v = Input.GetAxisRaw("Vertical1");

		movement.Set(0f, 0f, v);
		movement = movement.normalized * factor;
		cubeRigidbody.MovePosition(transform.position + movement);
		depth = transform.position.z;


		Depth_C.text = "Depth_C: " + depth.ToString() + " u";
		//Width_Cube.text = "Width_Cube: " + width.ToString() + " u";
		//Height_Cube.text = "Width_Cube: " + height.ToString() + " u";
	}
}
