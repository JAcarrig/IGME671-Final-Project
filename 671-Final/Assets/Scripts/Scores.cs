using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scores : MonoBehaviour {

    public int lives = 3;
    public int score = 0;
    const float DELAY = 1f;
    private float prevTime;

    GameObject sb;
    GameObject life;
    GameObject ship;

    GameObject shield1;
    GameObject shield2;

	// Use this for initialization
	void Start () {
        sb = GameObject.Find("Scoreboard");
        life = GameObject.Find("Lives");
        prevTime = 0;
        ship = GameObject.FindGameObjectWithTag("ship");

        shield1 = GameObject.Find("shield1");
        shield2 = GameObject.Find("shield2");
	}
	
	// Update is called once per frame
	void Update () {
        UpdateScoreboard();
        //DamageFlash();
	}

    void UpdateScoreboard()
    {
        sb.GetComponent<Text>().text = "Score: " + score;
        //life.GetComponent<Text>().text = "Lives: " + lives;


        





        if (lives <= 0)
        {
            Debug.Log("Loading");

            SceneManager.LoadScene("GameOver");

            Scene Ascene = SceneManager.GetSceneByName("GameOver");

            SceneManager.SetActiveScene(Ascene);
        }
    }

    public void ReduceLife()
    {
        if(Time.fixedTime - prevTime >= DELAY)
        {
            ship.GetComponent<Ship2>().emitters[2].Play();
            lives--;
            prevTime = Time.fixedTime;
            if (lives == 2)
            {
                Destroy(shield1);
            }
            if(lives == 1)
            {
                Destroy(shield2);
            }
        }

        
    }

    void DamageFlash()
    {
        //bool color = false;

        if (Time.fixedTime - prevTime >= DELAY)
        {

            ship.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            ship.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //if (lives <= 0)
        //{
        //    Debug.Log("Loading");

        //    SceneManager.LoadScene("GameOver");

        //    Scene Ascene = SceneManager.GetSceneByName("GameOver");

        //    SceneManager.SetActiveScene(Ascene);
        //}


        //if(color == true)
        //{

        //    ship.GetComponent<SpriteRenderer>().color = Color.red;
        //    color = false;
        //}
        //else
        //{
        //    ship.GetComponent<SpriteRenderer>().color = Color.white;
        //    color = true;
        //}




    }
}
