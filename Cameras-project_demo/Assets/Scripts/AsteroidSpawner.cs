using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour {

    public  float timeBetweenAsteroidSpawns;
    public float timeBetweenRingSpawns;
    public  float maxRings;
    public  float maxAsteroids;
    private float timesincelastAsteoridSpawn;
    private float timesinceLastRingSpawn;

    private int asteoidCount;
    private int asteoridPooliterator;
    private int ringPooliterator;

    private      Rect FarplaneCoords;
    private List<GameObject> asteroidPool;
    private List<GameObject> ringPool;
    private      GameObject mainCam;
    public       GameObject[] asteroids;
    public       GameObject Ring; 

    public  AnimationCurve trajectory;
    
	// Use this for initialization
	void Start () {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        FarplaneCoords = FarPlaneDimensions(mainCam.GetComponent<Camera>());
        asteroidPool = new List<GameObject>();
        ringPool = new List<GameObject>();
        BasicBehaviour.trajectory = trajectory;

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - timesincelastAsteoridSpawn > timeBetweenAsteroidSpawns) {
            Spawn();
            timesincelastAsteoridSpawn = Time.time;
            
        }
        if (Time.time - timesinceLastRingSpawn > timeBetweenRingSpawns) {
            spawnRing();
            timesinceLastRingSpawn = Time.time;
        }
        Debug.DrawLine(mainCam.transform.position,new Vector3(FarplaneCoords.xMin,FarplaneCoords.yMin,mainCam.GetComponent<Camera>().farClipPlane));
	}

    private void Spawn() {
        if (asteroidPool.Count < maxAsteroids)
        {
            int randomAsteroid = Random.Range(0, asteroids.Length);
            GameObject asteroid = GameObject.Instantiate(asteroids[randomAsteroid]);

            asteroid.transform.position = (Vector3)Random.insideUnitCircle  + this.transform.position; //this.transform.position + new Vector3(Random.Range(FarplaneCoords.xMin, FarplaneCoords.xMax), Random.Range(FarplaneCoords.yMin, FarplaneCoords.yMax), 0);
			//Debug.Log (asteroid.transform.position);

            asteroid.transform.localScale = Vector3.one * Random.Range(0.01f,0.03f);
            asteroid.AddComponent<AsteroidBehaviour>();
            var col =asteroid.AddComponent<MeshCollider>();
            col.convex = true;
            col.isTrigger = true;


            asteroidPool.Add(asteroid);
        }
        else
        {
            asteroidPool[asteoridPooliterator].SetActive(true);
            asteroidPool[asteoridPooliterator].transform.position = (Vector3)Random.insideUnitCircle + this.transform.position; // this.transform.position + new Vector3(Random.Range(FarplaneCoords.xMin, FarplaneCoords.xMax), Random.Range(FarplaneCoords.yMin, FarplaneCoords.yMax), 0);
            asteroidPool[asteoridPooliterator].GetComponent<AsteroidBehaviour>().restartMovement();
            asteoridPooliterator++;
            if(asteoridPooliterator== asteroidPool.Count)
            {
                asteoridPooliterator = 0;
            }

        }

    
    }

    private void spawnRing()
    {
        if (ringPool.Count < maxRings)
        {

            GameObject ring = GameObject.Instantiate(Ring);

            ring.transform.position = this.transform.position;// this.transform.position + new Vector3(Random.Range(FarplaneCoords.xMin, FarplaneCoords.xMax), Random.Range(FarplaneCoords.yMin, FarplaneCoords.yMax), 0); ;
            ringPool.Add(ring);
        }
        else
        {
            ringPool[ringPooliterator].SetActive(true);
            ringPool[ringPooliterator].transform.position = this.transform.position;//this.transform.position + new Vector3(Random.Range(FarplaneCoords.xMin, FarplaneCoords.xMax), Random.Range(FarplaneCoords.yMin, FarplaneCoords.yMax), 0);
            ringPool[ringPooliterator].GetComponent<RingBehaviour>().restartMovement();
            ringPooliterator++;
            if (ringPooliterator == ringPool.Count)
            {
                ringPooliterator = 0;
            }

        }


    }

    Rect FarPlaneDimensions(Camera cam)
    {
        Rect r = new Rect();

        float a = (mainCam.GetComponent<Camera>().nearClipPlane + mainCam.GetComponent<Camera>().farClipPlane)/2;//get length
        float A = cam.fieldOfView * 0.5f;//get angle
        A = A * Mathf.Deg2Rad;//convert tor radians
        float h = (Mathf.Tan(A) * a);//calc height
        float w = h * cam.aspect;

        r.xMin = -w;
        r.xMax = w;
        r.yMin = -h;
        r.yMax = h;
        return r;
    }


}
