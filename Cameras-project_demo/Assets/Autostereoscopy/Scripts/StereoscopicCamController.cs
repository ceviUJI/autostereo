using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StereoscopicCamController : MonoBehaviour {
    private const float INTERPUPILARY_INCREMENT = 0.001f; //0.0005f
    public float INTERPUPILARY_DISTANCE = 0.01f; //0.01f

    public float SWEETSPOTLOCATION = 2.5f; //1.3f
    public float current_Interpupilary;

    private float current_Sweetspot;

    public float Current_Sweetspot
    {
        get { return current_Sweetspot; }
        set { current_Sweetspot = value; }
    }


    private float FOV;

    private float currentAdjustTime;

    public int focus = 1;

    private Camera[] cams;
    public Dictionary<GameObject,float> closestObjects;
    KeyValuePair<GameObject, float> closest;

    public Text Interpupilary;
    public Text FOV_Text;
    public Text SP_Text;
	//public int frameRate = 120; //fix the framerate




	// Use this for initialization
	void Start () {

		//Time.captureFramerate = frameRate; 
     
        cams = this.GetComponent<StereoscopicCamRackGenerator>().Cams;
        FOV = this.GetComponent<StereoscopicCamRackGenerator>().FOV;
        //current_Interpupilary = INTERPUPILARY_DISTANCE;
        current_Sweetspot = SWEETSPOTLOCATION;
        closestObjects = new Dictionary <GameObject,float>();

        
        ResetCameraPosition();

       
    }
	
	// Update is called once per frame
    void Update()
    {
        manageInput();
    }
    private void manageInput() {

        if (Input.GetKey(KeyCode.F))
        {
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                changeFOV(0.05f);
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                changeFOV(-0.05f);
            }
        }
       else if (Input.GetKey(KeyCode.K))
        {
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                changeSweetSpotLocation(0.05f);
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                changeSweetSpotLocation(-0.05f);

            }
           // resetSweetspot();
        }
        //Cambio interpopularity
        else if (Input.GetKey(KeyCode.I))
        {
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                current_Interpupilary += INTERPUPILARY_INCREMENT;
                changeInterpupilary();
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                current_Interpupilary -= INTERPUPILARY_INCREMENT;
           
                changeInterpupilary();
            }  
        }

        // else if (Input.GetKey(KeyCode.KeypadPlus))
        // {
        //     current_Interpupilary += INTERPUPILARY_INCREMENT;
        //     changeInterpupilary();
        // }
        // else if (Input.GetKey(KeyCode.KeypadMinus))
        // {
        //     current_Interpupilary -= INTERPUPILARY_INCREMENT;
        //
        //     changeInterpupilary();
        // } 
        else if (Input.GetKeyDown(KeyCode.R))
        {
           
            ResetCameraPosition();
           
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            focus *= -1;
            ResetCameraPosition();
            resetFOV(FOV);
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            FOV_Text.transform.parent.gameObject.SetActive(!FOV_Text.transform.parent.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            Time.timeScale = 4;
        }
        if(Input.GetKeyDown(KeyCode.L)){
            foreach (var c in cams){
                c.targetTexture =null;
            
            }
        }
        
        
    }
    public void ResetCameraPosition()
    {
        float interpup = PlayerPrefs.GetFloat("INTERPUPILARY_DISTANCE");

        current_Interpupilary = interpup == 0 ? INTERPUPILARY_DISTANCE : interpup;
        int i = 0;
        foreach (var cam in cams)
        {
            Vector3 currentPosition = this.transform.position;
            Vector3 XPos = this.transform.right * (current_Interpupilary * (i + 1 - 4));
            cam.transform.localPosition = XPos;
            if (focus > 0)
            {
                cam.transform.LookAt(this.transform.position + this.transform.forward.normalized * current_Sweetspot);
            }
            else
            {
                cam.transform.LookAt(this.transform.position + XPos + this.transform.forward.normalized * current_Sweetspot);
            }

            i++;
        }
        
		resetFOV(FOV);
        resetSweetspot();

        if (Interpupilary != null)
            Interpupilary.text = "INTERPUPILARY: " + current_Interpupilary + "u";

    }

    private void changeInterpupilary() {

        int i = 0;
        foreach (var cam in cams)
        {
            Vector3 currentPosition = this.transform.position;
            Vector3 XPos = this.transform.right * (current_Interpupilary * (i + 1 - 4));
            cam.transform.localPosition = XPos;
            if (focus > 0)
            {
                cam.transform.LookAt(this.transform.position + this.transform.forward.normalized * current_Sweetspot);
            }
            else
            {
                cam.transform.LookAt(this.transform.position + XPos + this.transform.forward.normalized * current_Sweetspot);
            }

            i++;
        }
        if (Interpupilary != null)
            Interpupilary.text = "INTERPUPILARY: " + current_Interpupilary  + "u";
    }
    private void changeFOV(float FOV)
    {
        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].fieldOfView += FOV;
        }
        if(FOV_Text!=null)FOV_Text.text = "FOV: " + cams[0].fieldOfView;

    }
    private void resetFOV(float FOV)
    {
        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].fieldOfView = FOV;
        }
        if (FOV_Text != null) FOV_Text.text = "FOV: " + cams[0].fieldOfView;

    }
    public void changeSweetSpotLocation(float distance) {
        current_Sweetspot += distance;
        foreach (var cam in cams)
        {
            if (focus > 0)
            {
                cam.transform.LookAt(this.transform.position + this.transform.forward.normalized * current_Sweetspot);
            }
        }
		if (SP_Text != null)
			SP_Text.text = "SP_D: " + current_Sweetspot + "u";

    
    }

    public void changeSweetSpotLocationDirectly(float newSP)
    {
        current_Sweetspot =newSP;
        foreach (var cam in cams)
        {
            if (focus > 0)
            {
               
                cam.transform.LookAt(this.transform.position + this.transform.forward.normalized * current_Sweetspot);
                if (SP_Text != null) SP_Text.text = "SP_D: " + current_Sweetspot + "u";
            }
        }
		if (focus < 0) SP_Text.text = "SP_D: infinite";


    }
	
    private void resetSweetspot()
    {
       
        current_Sweetspot = SWEETSPOTLOCATION;
        foreach (var cam in cams)
        {
            if (focus > 0)
            {
                cam.transform.LookAt(this.transform.position + this.transform.forward.normalized * current_Sweetspot);
                if (SP_Text != null) SP_Text.text = "SP_D: " + current_Sweetspot + "u";
            }
        }

        if (focus < 0) SP_Text.text = "SP_D: infinite";

    }
    public void changeSweetspotOvertime(float time,float newSP) { 
        StartCoroutine(changeSweetspotRoutine(time,newSP));
    
    }
    IEnumerator changeSweetspotRoutine(float time,float newSP) {
        float lastSweetSpot = current_Sweetspot;
        for (float i = 0.0f; i < 1.0; i+=Time.deltaTime/time)
        {
            current_Sweetspot = Mathf.Lerp(lastSweetSpot, newSP, i);
            yield return null;
        }
    
    }
}
