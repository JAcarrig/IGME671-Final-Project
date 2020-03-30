using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {

    private float lifetime;
    private float prevtime;


    private Vector3 direction;
    private const float SPEED = .05f;


    private GameObject target;
    private GameObject[] targetArray;
    private GameObject manager;

	// Use this for initialization
	void Start () {
        prevtime = Time.fixedTime;

        direction = new Vector3(0, 1);

        targetArray = GameObject.FindGameObjectsWithTag("Planet");
        target = targetArray[Random.Range(0, targetArray.Length)];

        manager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {

        //FreeMove();
        Tracker();


        //if (Time.fixedTime - prevtime < .5)
        //{
        //    FreeMove();
        //    //lifetime += Time.fixedTime;
        //    //Debug.Log(lifetime);
        //}
        //else
        //{
        //    Tracker();
        //}



    }

    //correct initial missle alignment
    void FreeMove()
    {
        //direction = transform.rotation.eulerAngles; 
        float angle = transform.rotation.z;
        angle *= Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);

        

        Debug.Log(angle);
        Debug.Log(direction);

        Vector3.Normalize(direction);

        Vector3 velocity = SPEED * direction;

        //Debug.Log(velocity);

        transform.position += velocity;

    }

    //figure out why the missles dont turn correctly
    void Tracker()
    {
        if(target == null)
        {
            targetArray = GameObject.FindGameObjectsWithTag("Planet");
            target = targetArray[Random.Range(0, targetArray.Length)];
        }

        //Fint MT -- M to T
        Vector3 M = transform.position;
        Vector3 T = target.transform.position;


        Vector3 targetVector = T - M;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x);
        Vector3.Normalize(targetVector);

        Vector3 velocity = targetVector * SPEED;
        transform.position += velocity;




        Debug.Log(targetVector);

        
        angle *= Mathf.Rad2Deg;
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        //float radius = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
        //float eRadius = target.GetComponent<SpriteRenderer>().bounds.extents.y;
       
        

        //if (Vector3.Distance(gameObject.transform.position, target.transform.position) < radius + eRadius)
        //{
        //    manager.GetComponent<Scores>().score += 20;
        //    target.GetComponent<AsteroidCollider>().Breakup();

        //    Destroy(gameObject);
        //}

    }
}
