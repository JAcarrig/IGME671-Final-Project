using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship2 : MonoBehaviour {


    public GameObject bullet;
    public GameObject missle;
    private GameObject points;

    List<GameObject> bulletList;
    Dictionary<GameObject, Vector3> Dirlist;
    List<GameObject> beamlist;

    //Movement Properties
    private Vector3 direction;
    public float speed;
    public float maxSpeed = 2;

    


    //2020-sound properties
    public FMODUnity.StudioEventEmitter[] emitters;

    private FMOD.Studio.EventInstance rotate, bigLaser, move, shoot;

    [FMODUnity.EventRef]
    public string Rotate, BasicLaser, BigLaser, Move, Launch;

    private bool soundUp = false;
    //[SerializeField]
    //[Range(0f, 1f)]
    //private float FMspeed;

    // Use this for initialization
    void Start () {
        direction = new Vector3(0, 1);

        bulletList = new List<GameObject>();
        Dirlist = new Dictionary<GameObject, Vector3>();
        beamlist = new List<GameObject>();
        points = GameObject.Find("GameManager");
        //2020 sound stuff
        emitters = GetComponents<FMODUnity.StudioEventEmitter>();

        //launch = FMODUnity.RuntimeManager.CreateInstance(Launch);
        rotate = FMODUnity.RuntimeManager.CreateInstance(Rotate);
        shoot = FMODUnity.RuntimeManager.CreateInstance(BasicLaser);
        bigLaser = FMODUnity.RuntimeManager.CreateInstance(BigLaser);
        move = FMODUnity.RuntimeManager.CreateInstance(Move);
        move.start();
        FMODUnity.RuntimeManager.CreateInstance("event:/Ambience/ambience").start();
    }

    // Update is called once per frame
    void Update()
    {

        if (VarTransfer.Paused == false)
        {
            doSound();
            CheckInputs();
        }
        Wrap();
        Exit();
    }

    #region Movement
    void CheckInputs()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Quaternion.Euler(0, 0, 3f) * direction;
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Quaternion.Euler(0, 0, -3f) * direction;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //MoveForward();
            speed += .005f;
        }
        else if(speed >= 0)
        {
            speed -= .005f;
        }

        if(speed < 0)
        {
            speed = 0;
        }
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        MoveForward();

        float angle = Mathf.Atan2(direction.y, direction.x);
        angle = angle * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Shoot(angle, direction);
        Missles(direction);
        MegaBeam(angle, direction);

    }

    void MoveForward()
    {

        Vector3 velocity = speed * direction;

        transform.position += velocity;
       
    }

    void Wrap()
    {
        Vector3 shipPos = transform.position;

        if(shipPos.x > 11)
        {
            shipPos.x = -11;
        }
        if(shipPos.x < -11)
        {
            shipPos.x = 11;
        }
        if(shipPos.y > 5)
        {
            shipPos.y = -5;
        }
        if(shipPos.y < -5)
        {
            shipPos.y = 5;
        }

        transform.position = shipPos;
    }
    #endregion

    #region sound

    void doSound()
    {
        //movement
        float FMspeed = (speed * 1) / .1f;
             
        move.setParameterByName("speed", FMspeed);
        //big laser
        if (Input.GetKeyDown(KeyCode.X) /*&& points.GetComponent<Scores>().score >= 40*/)
        {
            //emitters[1].Play();
            
            StartCoroutine(laserDelay());
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            //emitters[1].Stop();
            bigLaser.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            soundUp = false;
        }
        //rotate
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {  
            
            //emitters[0].Play();
            rotate.start();
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            //emitters[0].Stop();
            rotate.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        //move forward/back
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            emitters[4].Play();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {

        }



    }

    #endregion

    #region weapons
    /// <summary>
    /// Spawns projectile moving in same direction as ship when called
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="direction"></param>
    void Shoot(float angle, Vector3 direction)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.PlayOneShot(BasicLaser);

            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = gameObject.transform.position;

            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);


            //adds bullet to tracking list
            bulletList.Add(newBullet);
            //records current ship direction and assigns it to bullet, used for movement
            Dirlist.Add(newBullet, direction);
        }

        if (bulletList.Count > 0 && VarTransfer.Paused == false)
        {
            foreach (GameObject element in bulletList)
            {
                //Vector3 velocity = element.transform.rotation.eulerAngles * .01f;
                Vector3 velocity = .3f * Dirlist[element];


                //Debug.Log(angle);
                //Debug.Log(velocity);

                element.transform.position += velocity;

                if (Mathf.Abs(element.transform.position.x) > 12 || Mathf.Abs(element.transform.position.y) > 5)
                {
                    //Dirlist.Remove(element);
                    //bulletList.Remove(element);
                    //Destroy(element);
                    DestroyBullet(element);
                }


            }
        }
    }

    void Missles(Vector3 direction)
    {
        Vector3 reverse = /*Quaternion.Euler(0, 0, 270) **/ direction;

        //Debug.Log(points);
        if (Input.GetKeyDown(KeyCode.C) == true /*&& points.GetComponent<Scores>().score >= 500*/)
        {
            points.GetComponent<Scores>().score -= 500;//500
            for (int i = 0; i < 6; i++)
            {
                //launch.start();
                StartCoroutine(player());
                GameObject newMissle = Instantiate(missle);


                newMissle.transform.position = transform.position;
                float mAngle = Mathf.Atan2(reverse.y, reverse.x);
                //mAngle -= 180;
                mAngle *= Mathf.Rad2Deg;
                mAngle -= 270;

                //Debug.Log(i * 15);
                mAngle += i * 15;

                newMissle.transform.rotation = Quaternion.Euler(0, 0, mAngle);
            }
        }
        
    }
    /// <summary>
    /// Ship's Ultimate laser weapon
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="direction"></param>
    void MegaBeam(float angle, Vector3 direction)
    {
        bool beamText = gameObject.GetComponentInChildren<SpriteRenderer>().enabled;
        

        if (Input.GetKey(KeyCode.X) == true && soundUp == true /*&& points.GetComponent<Scores>().score >= 40*/)
        {
            //max length of viewport is 11
            beamText = true;
            


               


                

            

            points.GetComponent<Scores>().score -= 40;//40

            if (beamlist.Count > 0)
            {
                foreach(GameObject element in beamlist)
                {
                    Destroy(element);
                }
            }

            for (int i = 1; i < 50; i++)
            {
                float b = i;
                GameObject nb = Instantiate(bullet);

                nb.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                nb.transform.position = transform.position;
                nb.transform.position +=  direction * (b/2);

                beamlist.Add(nb);
            }
        }
        else
        {
            
            beamText = false;
            if (beamlist.Count > 0)
            {
                foreach (GameObject element in beamlist)
                {
                    Destroy(element);
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// Safley deletes bullets when colliding or out of bounds
    /// </summary>
    /// <param name="target">Bullet to be deleted</param>
    public void DestroyBullet(GameObject target)
    {
        Dirlist.Remove(target);
        bulletList.Remove(target);
        Destroy(target);
    }

    void Exit()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }
    /// <summary>
    /// missle launcher coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator player()
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("player start");
            yield return new WaitForSeconds(.08f);

            FMODUnity.RuntimeManager.PlayOneShot(Launch);
            //FMOD.Studio.EventInstance launch;
            //launch = FMODUnity.RuntimeManager.CreateInstance(Launch);
            //launch.start();
        }
    }

    IEnumerator laserDelay()
    {
        bigLaser.start();
        yield return new WaitForSeconds(0.4f);
        soundUp = true;

    }
}
