using UnityEngine;
using System.Collections;

public class Activate3D : MonoBehaviour {

    public Camera normalCamera;

    private bool is3DEnabled = true;

    public void OnClick3D()
    {
        if (is3DEnabled)
        {
          
            normalCamera.enabled = true;
            is3DEnabled = !is3DEnabled;
        }
        else
        {
           
            normalCamera.enabled = false;
            is3DEnabled = !is3DEnabled;

        }
    }

	// Use this for initialization
	void Start () {
        normalCamera.enabled = false;
       // cameras3D.SetActive(true);
	}  
	
	// Update is called once per frame
	void Update () {
	
	}
}
