using UnityEngine;
using System.Collections;

public class PiecesAnimation : MonoBehaviour {
    public static GameObject[] pieces;
    public GameObject[] newPieces;
    public Vector3 scale;
	// Use this for initialization
	void Start () {
        int i=0;
        foreach (var item in pieces)
        {
            GameObject asteroidpiece = GameObject.Instantiate(item);
            asteroidpiece.transform.localScale = scale;
            Vector3 pos = Random.insideUnitSphere;
            pos.x*=scale.x;
            pos.y*=scale.y;
            pos.z*=scale.z;
            asteroidpiece.transform.position = pos;
            newPieces[i] = asteroidpiece;
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator moveRandomObjects()
    {
        foreach (var item in newPieces)
        {
            yield return null;
        }

    }
}
