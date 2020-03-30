using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollider : MonoBehaviour {

    public GameObject smallAsteroid;
    private GameObject Ship;
    private GameObject manager;
    //private GameObject beam;

    private float radius;
    private Vector3 direction;
    private float angleDeg;
    private int aIndex;

    #region large asteroids
    private Sprite[] bigAsteroids;
    public Sprite a1;
    public Sprite a2;
    public Sprite a3;
    public Sprite a4;
    public Sprite a5;
    #endregion


    #region small asteroids
    private Sprite[] smallAsteroids;
    public Sprite sa1;
    public Sprite sa2;
    public Sprite sa3;
    public Sprite sa4;
    public Sprite sa5;
    #endregion
    
    

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("GameManager");
        radius = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        Ship = GameObject.FindGameObjectWithTag("ship");
        //beam = GameObject.Find("Beam");
        SpriteList();


        radius = radius - 1f;

        float angle = Random.Range(0, 360);
        angleDeg = angle;
        angle = angle * Mathf.Deg2Rad;
        //Gets random angle in degrees, converts to radians, then resolves a unit vector
        direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

        RandSprite();
	}
	
	// Update is called once per frame
	void Update () {
        BulletCollision();
        ShipCollision();
        Movement();
        
        //Gizmos.DrawSphere(transform.position, radius);
        //Gizmos.DrawSphere(Ship.transform.position, Ship.GetComponent<SpriteRenderer>().bounds.extents.x);
    }
    /// <summary>
    /// Detects bullet-asteroid collisions
    /// </summary>
    void BulletCollision()
    {
        GameObject[] bulletArray = GameObject.FindGameObjectsWithTag("bullet");

        foreach(GameObject element in bulletArray)
        {
            float eRadius = element.GetComponent<SpriteRenderer>().bounds.extents.y;

            if(Vector3.Distance(gameObject.transform.position, element.transform.position) < radius + eRadius)
            {
                manager.GetComponent<Scores>().score += 20;
                Ship.GetComponent<Ship2>().DestroyBullet(element);
                Breakup();
            }
        }

        GameObject[] missleArray = GameObject.FindGameObjectsWithTag("missle");

        foreach (GameObject element in missleArray)
        {
            float eRadius = element.GetComponent<SpriteRenderer>().bounds.extents.y;

            if (Vector3.Distance(gameObject.transform.position, element.transform.position) < radius + eRadius + .5f)
            {
                manager.GetComponent<Scores>().score += 20;
                //Ship.GetComponent<Ship2>().DestroyBullet(element);
                Destroy(element);
                Breakup();
            }
        }

        //if(beam.GetComponent<SpriteRenderer>().enabled == true)
        //{
            
        //}


    }


    /// <summary>
    /// Detects if asteroid has collided with ship
    /// </summary>
    void ShipCollision()
    {
        //Debug.DrawLine(gameObject.transform.position, Ship.transform.position);

        float sRadius = Ship.GetComponent<SpriteRenderer>().bounds.extents.x;
        sRadius = sRadius - .5f;
        float shipDist = Vector3.Distance(Ship.transform.position, gameObject.transform.position);
        


        if(shipDist < sRadius + radius)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            manager.GetComponent<Scores>().ReduceLife();
        }
        
        
    }
    /// <summary>
    /// breaks asteroid into 2 smaller asteroids
    /// </summary>
    public void Breakup()
    {
        //create first child
        GameObject active = Instantiate(smallAsteroid);
        active.transform.position = gameObject.transform.position;
        active.GetComponent<SpriteRenderer>().sprite = smallAsteroids[aIndex];

        
        active.GetComponent<SmallMove>().angle = angleDeg + 15;

        //create second child
        active = Instantiate(smallAsteroid);
        active.transform.position = gameObject.transform.position;
        active.GetComponent<SpriteRenderer>().sprite = smallAsteroids[aIndex];

        
        active.GetComponent<SmallMove>().angle = angleDeg - 15;

        //delete parent asteroid
        Destroy(gameObject);
    }

    void Movement()
    {
        Vector3 Pos = gameObject.transform.position;
        Vector3 velocity = direction * .01f;
        gameObject.transform.position += velocity;

        
        if (Pos.x > 11f)
            gameObject.transform.position = new Vector3(-11, Pos.y, 0);
        if (Pos.x < -11f)
            gameObject.transform.position = new Vector3(11, Pos.y, 0);
        if (Pos.y > 6f)
            gameObject.transform.position = new Vector3(Pos.x, -6, 0);
        if (Pos.y < -6f)
            gameObject.transform.position = new Vector3(Pos.x, 6, 0);
    }

    void SpriteList()
    {
        bigAsteroids = new Sprite[] { a1, a2, a3, a4, a5 };
        smallAsteroids = new Sprite[] { sa1, sa2, sa3, sa4, sa5 };
    }

    void RandSprite()
    {
        int i = Random.Range(0, 4);
        aIndex = i;
        gameObject.GetComponent<SpriteRenderer>().sprite = bigAsteroids[i];
    }
}
