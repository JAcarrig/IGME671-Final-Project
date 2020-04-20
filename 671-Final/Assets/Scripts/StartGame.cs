using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        BeginGame();
	}

    void BeginGame()
    {
        if(Input.GetKeyDown(KeyCode.K) == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
            VarTransfer.Kmode = !VarTransfer.Kmode;
            bool km = VarTransfer.Kmode;

            if(km == true)
            {
                GameObject.Find("Toggle").GetComponent<Text>().text = "ON";
            }
            else
            {
                GameObject.Find("Toggle").GetComponent<Text>().text = "OFF";
            }
        }





        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/select");
            Debug.Log("Loading");

            SceneManager.LoadScene("Asteroids");

            Scene Ascene = SceneManager.GetSceneByName("Asteroids");
            
            SceneManager.SetActiveScene(Ascene);
        }
    }
}
