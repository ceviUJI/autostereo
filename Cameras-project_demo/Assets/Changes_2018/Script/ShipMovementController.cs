using UnityEngine;
using System.Collections;

public class ShipMovementController : MonoBehaviour {

    public GameObject ship;
    
    private ShipMovement automaticMovement;
    private ShipMovementGame manualMovement;
    bool automaticaActivate;

    private float timer;

	// Use this for initialization
	void Start () {

        automaticMovement = ship.GetComponent<ShipMovement>();
        manualMovement = ship.GetComponent<ShipMovementGame>();
        //Debug.Log("AUTOMATIC SHIP");
       
        manualMovement.enabled = false;
        automaticMovement.enabled = true;
        automaticaActivate = true;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if(timer >= 22 && timer < 23)
        {
            automaticaActivate = false;
            //Debug.Log("MANUAL SHIP");
            manualMovement.enabled = true;
            automaticMovement.enabled = false;
        }

       // if (Input.GetKey(KeyCode.K)){
       //     if (!automaticaActivate)
       //     {
       //         automaticaActivate = true;
       //         Debug.Log("AUTOMATIC SHIP");
       //         manualMovement.enabled = false;
       //         automaticMovement.enabled = true;
       //     }
       //     else
       //     {
       //         automaticaActivate = false;
       //         Debug.Log("MANUAL SHIP");
       //         manualMovement.enabled = true;
       //         automaticMovement.enabled = false;
       //     }
       //   
       //    
       // }

	
	}
}
