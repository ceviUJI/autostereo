using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StereoscopicCamRackGenerator : MonoBehaviour
{

    #region Private_Var_Reg

    public static float INTERPUPILLARY_DISTANCE = 0.01f;
    public static float SWEETSPOTLOCATION = 2.5f; //2.5f
    private Camera[] cams = new Camera[8];
   

    #endregion Private_Var_Reg

    #region public_Var_Reg

         [Tooltip("This is the camera that will turn in to 8 cameras Rendering to texture")]
         public Camera CameraToBeReplicated;
         public Camera[] Cams  { get { return cams; }}
         public float fov;//23.402f;

         public float FOV
         {
             get { return fov; }
         }

    #endregion public_Var_Reg

    void Start() {
        float interpup = PlayerPrefs.GetFloat("INTERPUPILARY_DISTANCE");
        INTERPUPILLARY_DISTANCE = interpup == 0 ? INTERPUPILLARY_DISTANCE : interpup;
        for (int i =0; i <8; i += 2)
       {
           cams[i] = instantiateCam(i, CameraToBeReplicated.gameObject);
           cams[i + 1] = instantiateCam(i + 1, CameraToBeReplicated.gameObject);
       }
       GameObject.Destroy(CameraToBeReplicated.gameObject);

    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (cams[0] != null)
        {
            foreach (var c in cams)
            {
                Gizmos.DrawLine(c.transform.position, c.transform.position + c.transform.forward * float.MaxValue);
            }
        }
    }
 
    private void checkPreConditions() {
        if (CameraToBeReplicated.targetTexture == null)
        {
            Debug.LogError("CameraToBeReplicated must be rendering to Texture!!");
            Debug.Break();
        }
    }
    private Camera instantiateCam(int i, GameObject original)
    {
        Camera cam;
        Vector3 currentPosition = original.transform.position;
        float sploc = PlayerPrefs.GetFloat("SWEETSPOTLOCATION");


        cam = Camera.Instantiate(original.GetComponent<Camera>(), new Vector3(currentPosition.x + INTERPUPILLARY_DISTANCE * (i + 1 - 4), currentPosition.y, currentPosition.z), new Quaternion()/*, Quaternion.FromToRotation(new Vector3(0, 0, 0), new Vector3(0, 0, 1))*/) as Camera;
        //puts the focus point on the 
        cam.transform.LookAt(original.transform.position + original.transform.forward.normalized * (sploc==0? SWEETSPOTLOCATION:sploc));

        cam.transform.parent = this.transform;
        Vector4 Viewport = configureViewport(7-i);

        cam.rect = new Rect(Viewport.x, Viewport.y, Viewport.z, Viewport.w);

        cam.name = "Cam_" + i;
        cam.tag = "StereoscopicCam";

        return cam;

    }

    private Vector4 configureViewport(int i)
    {
        switch (i)
        {
            case 0:
                return new Vector4(0, 0.75f, 0.5f, 0.25f);

            case 1:
                return new Vector4(0.5f, 0.75f, 0.5f, 0.25f);

            case 2:
                return new Vector4(0, 0.5f, 0.5f, 0.25f);

            case 3:
                return new Vector4(0.5f, 0.5f, 0.5f, 0.25f);

            case 4:
                return new Vector4(0, 0.25f, 0.5f, 0.25f);

            case 5:
                return new Vector4(0.5f, 0.25f, 0.5f, 0.25f);

            case 6:
                return new Vector4(0, 0f, 0.5f, 0.25f);

            case 7:
                return new Vector4(0.5f, 0f, 0.5f, 0.25f);
        }
        return new Vector4();
    }
}
