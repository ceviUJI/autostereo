using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerOfThings : MonoBehaviour {

    public float timeBetweenAsteroidSpawn;
    public int maxAsteroids;

    public GameObject[] typesAsteroids;

    private float timeSinceLastAsteroidSpawn;
    private int asteroidCount;
    private int asteroidPoolIterator;
    public static List<GameObject> asteroidPool;

    public float distanceOfCreation;

    public AnimationCurve trajectory;


	// Use this for initialization
	void Start () {

        asteroidPool = new List<GameObject>();

        BasicBehaviour.trajectory = trajectory;

        for (int i = 0; i < 30; i++)
        {
            Spawn();
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.time - timeSinceLastAsteroidSpawn > timeBetweenAsteroidSpawn)
        {
            Spawn();
            timeSinceLastAsteroidSpawn = Time.time;
        }
       
	
	}

    private void Spawn()
    {
        if (asteroidPool.Count < maxAsteroids)
        {
            int randomAsteroid = Random.Range(0, typesAsteroids.Length);
            GameObject asteroid = GameObject.Instantiate(typesAsteroids[randomAsteroid]);

            //asteroid.transform.position = (Vector3)Random.insideUnitCircle + this.transform.position;

            asteroid.transform.position = (Vector3)Random.insideUnitCircle + this.transform.position;
           // asteroid.transform.position = new Vector3(Random.value, Random.value, distanceOfCreation);

            //asteroid.transform.position = Camera.main.ViewportToWorldPoint(asteroid.transform.position);

            asteroid.transform.localScale = Vector3.one * Random.Range(0.3f, 0.8f);
            //asteroid.AddComponent<AsteroidBehaviour>();
            asteroid.AddComponent<AsteroidMovement>();
            var col = asteroid.AddComponent<MeshCollider>();
            col.convex = true;
            col.isTrigger = true;

            asteroidPool.Add(asteroid);
        }

    }

}
