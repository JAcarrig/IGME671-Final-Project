using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_TEST : MonoBehaviour
{
    private FMOD.Studio.EventInstance move;
    private float dt;


    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField] [Range(0f, 1f)]
    private float speed;


    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        once = false;
        move = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        //move.start();
    }

    // Update is called once per frame
    void Update()
    {
        //move.setParameterByName("speed", speed);

        if (once == false)
        {
            
            
                
                once = true;
               
                StartCoroutine(player());
                
            
        }
        
    }

    IEnumerator player()
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("player start");
            yield return new WaitForSeconds(.16f);

            FMOD.Studio.EventInstance launch;
            launch = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
            launch.start();
        }
    }
}
