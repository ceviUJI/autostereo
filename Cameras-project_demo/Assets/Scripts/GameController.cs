using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

    public AnimationCurve DamageEffect;
    public GameObject FSQ;
    public GameObject CamRack;
    Material FSQMat;
    public StereoscopicCamRackGenerator generator;
    public StereoscopicCamController camcontroller;
    public ShipMovement playerMovement;
   

    public enum ConvergenceAccomodationExperiments
    {
        VaryingConvergence,StaticConvergence, Planar,configuration
    }
    [SerializeField]
    public ConvergenceAccomodationExperiments experiment;
    public float shake = 0;
    float shakeAmount = 0.004f;
    float decreaseFactor  = 1.0f;
	// Use this for initialization
	void Start () {
        FSQMat = FSQ.GetComponent<MeshRenderer>().sharedMaterial;
        
        switch (experiment)
        {
            case ConvergenceAccomodationExperiments.VaryingConvergence:
                playerMovement.moveConvergencePlane = true;
                break;
            case ConvergenceAccomodationExperiments.StaticConvergence:
                playerMovement.moveConvergencePlane = false;
                if (PlayerPrefs.GetFloat("SWEETSPOTLOCATION") != 0)
                {
                    camcontroller.Current_Sweetspot = PlayerPrefs.GetFloat("SWEETSPOTLOCATION");
                }
                else
                {
                    camcontroller.Current_Sweetspot = 5.3f;
                }
                
                break;
            case ConvergenceAccomodationExperiments.Planar:
                camcontroller.focus *= -1;
               
                break;
            case ConvergenceAccomodationExperiments.configuration:
                playerMovement.moveConvergencePlane = true;
                PlayerPrefs.SetFloat("INTERPUPILARY_DISTANCE", 0.01f);
                Time.timeScale = 1;
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        doScreenShake();
        handleInput();
	}
    public void animateDamage(float animationTime) {
        StartCoroutine(AnimateDamage(animationTime));
    }

    IEnumerator AnimateDamage(float time)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            float v =DamageEffect.Evaluate(t);
            yield return null;
        }
    }

    public void doScreenShake() {

        if (shake > 0)
        {
            CamRack.transform.localPosition = CamRack.transform.position+ Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0.0f;
        }
    }

    private void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 1;
            Application.LoadLevel("ConfigurationScene");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale = 1;
            PlayerPrefs.SetFloat("INTERPUPILARY_DISTANCE", camcontroller.current_Interpupilary);
            Application.LoadLevel("Experiment1");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Time.timeScale = 1;
            Application.LoadLevel("Experiment2");

        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Time.timeScale = 1;
            Application.LoadLevel("Experiment3");
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            Time.timeScale = 1;

            Application.LoadLevel("TerrainExperiment1");
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            Time.timeScale = 1;


            Application.LoadLevel("TerrainExperiment2");
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            Time.timeScale = 1;

            Application.LoadLevel("TerrainExperiment3");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 0.5f;
            
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale = 1;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Time.timeScale =2f;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Time.timeScale = 4f;

            }
            
        }
    }
}
