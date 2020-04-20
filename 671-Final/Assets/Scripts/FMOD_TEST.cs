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
    private float volume = 1;

    [SerializeField]
    private bool pause;

    FMOD.Studio.Bus SFX;

    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        once = false;
        move = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        //move.start();
    }

    // Update is called once per frame
    void Update()
    {
        float vol = 100;
        //move.setParameterByName("speed", speed);
        //SFX.setVolume(volume);
        //SFX.getVolume(out vol);
        //SFX.setPaused(pause);
        //Debug.Log("vol: " + vol);
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    pause = !pause;
        //    SFX.setPaused(pause);
        //}
        //else
        //{
        //    SFX.setPaused(false);
        //}


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
