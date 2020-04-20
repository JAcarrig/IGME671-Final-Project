using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{

    //public GameObject canvas;
    public GameObject pauseUI;
    FMOD.Studio.Bus SFX,AMB;

    private int sfxVol;
    private int ambVol = 100;
    private int sel = 0;
    private TextMeshProUGUI sfxUI;
    //private TextMeshProUGUI ambUI;

    private void Awake()
    {
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        AMB = FMODUnity.RuntimeManager.GetBus("bus:/AMB");
    }


    // Start is called before the first frame update
    void Start()
    {
        sfxVol = VarTransfer.SVol;
        sfxUI = pauseUI.transform.Find("sfx").gameObject.GetComponent<TextMeshProUGUI>();
        sfxUI.text = $"SFX: {sfxVol}%";
        //ambUI = pauseUI.transform.Find("amb").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        SFX.setPaused(VarTransfer.Paused);
        AMB.setPaused(VarTransfer.Paused);
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            VarTransfer.Paused = !VarTransfer.Paused;
            if(VarTransfer.Paused)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/onpause");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/offpause");
            }


            pauseCheck();
        }

        if(VarTransfer.Paused == true)
        {
            pauseUpdate();
        }
    }

    void pauseCheck()
    {
        if (VarTransfer.Paused == true)
        {
            pauseUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);
        }
    }

    void pauseUpdate()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //switch (sel)
            //{
                //case 0:
                    sfxVol += 10;
                    if (sfxVol > 100)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switchlimit");
                        sfxVol = 100;
                    }
                    else
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
                    }
                    sfxUI.text = $"SFX: {sfxVol}%";
                    //break;
                //case 1:
                    //ambVol += 10;
                    //if (sfxVol > 100)
                    //{
                    //    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switchlimit");
                    //    ambVol = 100;
                    //}
                    //else
                    //{
                    //    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
                    //}
                    //ambUI.text = $"SFX: {ambVol}%";
                    //break;
            //}
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sfxVol -= 10;
            if (sfxVol < 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switchlimit");
                sfxVol = 0;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
            }
            sfxUI.text = $"SFX: {sfxVol}%";

        }

        SFX.setVolume(sfxVol * .01f);
        VarTransfer.SVol = sfxVol;
    }
}
