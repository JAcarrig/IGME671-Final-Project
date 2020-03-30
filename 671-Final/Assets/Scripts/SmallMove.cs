using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMove : MonoBehaviour {

    /// <summary>
    /// Angle of velocity vector, in degrees
    /// </summary>
    public float angle;
    private float radius;
    private Vector3 direction;

    GameObject Ship;
    GameObject manager;

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("GameManager");
        radius = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        Ship = GameObject.FindGameObjectWithTag("ship");


        angle = angle * Mathf.Deg2Rad;
		direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        
        if (VarTransfer.Kmode == false)
        {
            BulletCollision();
        }
        ShipCollision();
	}

    void Movement()
    {
        float speed = .01f;
        if(VarTransfer.Kmode == true)
        {
            speed = .08f;
        }




        Vector3 Pos = gameObject.transform.position;
        Vector3 velocity = direction * speed;
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

    void BulletCollision()
    {
        GameObject[] bulletArray = GameObject.FindGameObjectsWithTag("bullet");

        foreach (GameObject element in bulletArray)
        {
            float eRadius = element.GetComponent<SpriteRenderer>().bounds.extents.y;

            if (Vector3.Distance(gameObject.transform.position, element.transform.position) < radius + eRadius)
            {
                manager.GetComponent<Scores>().score += 50;
                Ship.GetComponent<Ship2>().DestroyBullet(element);
                Destroy(gameObject);
            }
        }


    }

    void ShipCollision()
    {
        Debug.DrawLine(gameObject.transform.position, Ship.transform.position);

        float sRadius = Ship.GetComponent<SpriteRenderer>().bounds.extents.x;
        sRadius = sRadius - .5f;
        float shipDist = Vector3.Distance(Ship.transform.position, gameObject.transform.position);

        if (shipDist < sRadius + radius)
        {
            manager.GetComponent<Scores>().ReduceLife();
        }
        

    }
}
