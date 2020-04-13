using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_TEST : MonoBehaviour
{
    private FMOD.Studio.EventInstance move;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField] [Range(0f, 1f)]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        move = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        move.start();
    }

    // Update is called once per frame
    void Update()
    {
        move.setParameterByName("speed", speed);
    }
}
