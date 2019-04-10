using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {

    private float angularSpeed = 90;
    private float AnimationTime = 0.5f;
    private Vector3 rotationangle;
    private float baseSpeed;
    Color baseColor;

    private GameObject player;

    private float speed;

    // Use this for initialization
    void Start () {


        baseSpeed = 0.8f ;
        speed *= Random.Range(0.75f, 1.25f);
        rotationangle = Vector3.right * Random.Range(0f, 1f) + Vector3.up * Random.Range(0f, 1f) + Vector3.forward * Random.Range(0f, 1f);

        player = GameObject.FindGameObjectWithTag("Player");
        
        
    }

    void animateCollision()
    {

        StartCoroutine("fadeAnim");
    }

    IEnumerator fadeAnim()
    {

        for (float i = 0.0f; i <= 1.0f; i += Time.deltaTime / AnimationTime)
        {
            //mat.color = Color.Lerp(baseColor, baseColor + new Color(0, 0, 0, -baseColor.a), i);
            yield return null;
        }

    }

    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {

            Debug.Log("Collision");
            //animateCollision();
            //Destroy(this);
            SpawnerOfThings.asteroidPool.Remove(this.gameObject);
            Destroy(this.gameObject, 0.5f) ;
           

        }
    }


    // Update is called once per frame
    void Update () {
        Move();
	}

    void Move()
    {
        this.transform.Rotate(rotationangle, angularSpeed * Time.deltaTime);
        this.transform.Translate(Vector3.back * 0.15f);

       //if(transform.position.z < Camera.main.transform.position.z)
       //{
       //    SpawnerOfThings.asteroidPool.Remove(this.gameObject);
       //    Destroy(this.gameObject, 0.5f);
       //}
    }
}
