using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : BasicBehaviour {

    public static GameObject apiece1,apiece2,apiece3;

    private float angularSpeed = 90;
    private float AnimationTime = 0.5f;
    private Vector3 rotationangle;
    private float baseSpeed;
    Color baseColor;

    private GameObject gamecontroller;

	// Use this for initialization
	void Start () {
        Speed *= 0.8f;
        base.configureParams();
        base.mat = this.GetComponent<MeshRenderer>().material;
        baseSpeed = Speed;
        Speed *= Random.Range(0.75f, 1.25f);
        rotationangle = Vector3.right * Random.Range(0f, 1f) + Vector3.up * Random.Range(0f, 1f) + Vector3.forward * Random.Range(0f, 1f);
        baseColor = mat.color;

        gamecontroller = GameObject.Find("GameController");
    }
	
	// Update is called once per frame
	void Update () {
        move();
    }
    public override void restartMovement() {
        base.restartMovement();
        Speed =baseSpeed* Random.Range(0.75f, 1.25f);     
        rotationangle = Vector3.right * Random.Range(0f, 1f) + Vector3.up * Random.Range(0f, 1f) + Vector3.forward * Random.Range(0f, 1f);
        mat.color = baseColor;
    }
    
    public override void move()
    {
        base.move();
        this.transform.Rotate(rotationangle, angularSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider c) {

        if(c.tag == "Player")
        {

            controller.animateDamage(AnimationTime);
            controller.shake = AnimationTime;
           // this.gameObject.SetActive(false);
            animateCollision();
            //Destroy(this, 0.5f);
            //Debug.Log("Colision asteroide");
            gamecontroller.GetComponent<GameView>().GetDamage();
           
        }
    }

    void animateCollision()
    {

        StartCoroutine("fadeAnim");
    }

    IEnumerator fadeAnim()
    {
      
        for (float i = 0.0f; i <= 1.0f; i+= Time.deltaTime/AnimationTime)
        {
            //mat.color = Color.Lerp(baseColor, baseColor + new Color(0, 0, 0, -baseColor.a), i);
            yield return null;
        }

    }
}
