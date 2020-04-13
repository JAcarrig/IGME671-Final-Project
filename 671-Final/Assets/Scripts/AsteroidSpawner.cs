using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject asteroid;

	// Use this for initialization
	void Start () {
        //SpawnAsteroids(20);
	}
	
	// Update is called once per frame
	void Update () {
        //checkAsteroidNum();
	}

    void SpawnAsteroids(int amount)
    {
        for(int i = 0; i < amount; i++)
        {

            int randX = Random.Range(-10, 10);
            int randY = Random.Range(-5, 5);

            GameObject newRock = Instantiate(asteroid);

            newRock.transform.position = new Vector3(randX, randY);

        }
    }

    void checkAsteroidNum()
    {
        int asteroidNum = GameObject.FindGameObjectsWithTag("Planet").Length;

        if(asteroidNum < 5)
        {
            //SpawnAsteroids(1);

            //int randX = Random.Range(-10, 10);
            int randX = -11;
            int randY = Random.Range(-5, 5);

            GameObject newRock = Instantiate(asteroid);

            newRock.transform.position = new Vector3(randX, randY);
        }

    }
}
