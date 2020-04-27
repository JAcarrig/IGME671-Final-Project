using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{

    //public GameObject canvas;
    public GameObject pauseUI;
    FMOD.Studio.Bus SFX,AMB,UI;

    private int sfxVol, ambVol, uiVol;
    //private int ambVol = 100;
    private int sel = 0;
    private TextMeshProUGUI sfxUI, ambUI, interfaceUI; 

    private void Awake()
    {
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        AMB = FMODUnity.RuntimeManager.GetBus("bus:/AMB");
        UI = FMODUnity.RuntimeManager.GetBus("bus:/UI");

    }


    // Start is called before the first frame update
    void Start()
    {
        sfxVol = VarTransfer.SVol;
        ambVol = VarTransfer.AVol;
        uiVol = VarTransfer.UVol;
        sfxUI = pauseUI.transform.Find("sfx").gameObject.GetComponent<TextMeshProUGUI>();
        sfxUI.text = $"SFX: {sfxVol}%";
        ambUI = pauseUI.transform.Find("amb").gameObject.GetComponent<TextMeshProUGUI>();
        ambUI.text = $"Ambience: {ambVol}%";
        interfaceUI = pauseUI.transform.Find("ui").gameObject.GetComponent<TextMeshProUGUI>();
        interfaceUI.text = $"UI: {uiVol}%";
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
            menuManager();
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

    void menuManager()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            sel--;
            if( sel < 0) { sel = 0; }
            else { FMODUnity.RuntimeManager.PlayOneShot("event:/UI/scroll"); }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sel++;
            if(sel > 2) { sel = 2; }
            else { FMODUnity.RuntimeManager.PlayOneShot("event:/UI/scroll"); }
        }

        switch (sel)
        {
            case 0:
                sfxUI.color = Color.yellow;
                ambUI.color = Color.white;
                interfaceUI.color = Color.white;

                sfxVol = pauseUpdate(sfxVol);
                sfxUI.text = $"SFX: {sfxVol}%";
                SFX.setVolume(sfxVol * .01f);
                VarTransfer.SVol = sfxVol;
                break;
            case 1:
                sfxUI.color = Color.white;
                ambUI.color = Color.yellow;
                interfaceUI.color = Color.white;

                ambVol = pauseUpdate(ambVol);
                ambUI.text = $"Ambience: {ambVol}%";
                AMB.setVolume(ambVol * .01f);
                VarTransfer.AVol = sfxVol;
                break;
            case 2:
                sfxUI.color = Color.white;
                ambUI.color = Color.white;
                interfaceUI.color = Color.yellow;

                uiVol = pauseUpdate(uiVol);
                interfaceUI.text = $"UI: {uiVol}%";
                UI.setVolume(uiVol * .01f);
                VarTransfer.UVol = uiVol;
                break;
        }


        
    }

    void pauseUpdate()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (sel)
            {
                case 0:
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
                    break;
                case 1:
                    ambVol += 10;
                    if (sfxVol > 100)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switchlimit");
                        ambVol = 100;
                    }
                    else
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
                    }
                    ambUI.text = $"SFX: {ambVol}%";
                    break;
            }
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

    int pauseUpdate(int cat)
    {
        int newVol = cat;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            newVol += 10;
            if (newVol > 100)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switchlimit");
                newVol = 100;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
            }
            
                 
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            newVol -= 10;
            if (newVol < 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switchlimit");
                newVol = 0;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/switch");
            }
            

        }

        return newVol;
    }
}
