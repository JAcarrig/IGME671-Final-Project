using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

    public GameObject Ship;
    private GameObject[] planetlist;
    private float ShipRadius;
    private bool DetMethod;



    // Use this for initialization
    void Start () {
        planetlist = GameObject.FindGameObjectsWithTag("Planet");
        Debug.Log("Detector Online");
        ShipRadius = Ship.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        DetMethod = true;
	}

    
    // Update is called once per frame
    void Update () {
        AABB();
        //BoundingCircles();


        //Code to enable switching
        //if(Input.GetKeyDown(KeyCode.Alpha1) == true || Input.GetKeyDown(KeyCode.Alpha2) == true)
        //{
        //    Debug.Log("keypress");
        //    DetMethod = keySwitcher();
        //}

        //if(DetMethod == true)
        //{
        //    AABB();
        //}
        //else
        //{
        //    BoundingCircles();
        //}

	}

    /// <summary>
    /// Collision Detection using Axis Aligned Bounding Box
    /// </summary>
    void AABB()
    {
        Bounds shipBounds = Ship.GetComponent<SpriteRenderer>().sprite.bounds;

       
        bool anycollisions = false;

        foreach (GameObject element in planetlist)
        {
            Bounds Ebound = element.GetComponent<SpriteRenderer>().sprite.bounds;

            Vector3 Epos = element.transform.position;
            Vector3 Spos = Ship.transform.position;

            float Ax = Spos.x - shipBounds.extents.x;
            float AX = Spos.x + shipBounds.extents.x;
            float Ay = Spos.y - shipBounds.extents.y;
            float AY = Spos.y + shipBounds.extents.y;

            float Bx = Epos.x - Ebound.extents.x;
            float BX = Epos.x + Ebound.extents.x;
            float By = Epos.y - Ebound.extents.y;
            float BY = Epos.y + Ebound.extents.y;

            

            //check if ship is within x bounds
            if ((Bx < Ax && BX > Ax) || (Bx < AX && BX > AX))
            {
                Debug.Log("first check pass");
                //check if ship is within y bounds
                if( (By < Ay && BY > Ay) || (By < AY && BY > AY))
                {
                    Debug.Log("second check pass");
                    //check successful - ship is colliding
                    element.GetComponent<SpriteRenderer>().color = Color.red;
                    Ship.GetComponent<SpriteRenderer>().color = Color.red;
                    anycollisions = true;
                }
                else
                {
                    //second check fail
                    element.GetComponent<SpriteRenderer>().color = Color.white;
                    if (anycollisions == false)
                    {
                        Ship.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
            }
            else
            {
                //first check fail
                element.GetComponent<SpriteRenderer>().color = Color.white;
                if (anycollisions == false)
                {
                    Ship.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }


        }
    }

    #region BoundingCircles
    /// <summary>
    /// Collision Detection using bounding circles
    /// </summary>
    void BoundingCircles()
    {
        bool anycollisions = false;

        foreach (GameObject element in planetlist)
        {
            SpriteRenderer current = element.GetComponent<SpriteRenderer>();
            SpriteRenderer ShipSprite = Ship.GetComponent<SpriteRenderer>();

            float Erad = current.sprite.bounds.extents.x;

            float dist = Vector3.Distance(Ship.transform.position, current.transform.position);

            
            if(dist < (Erad + ShipRadius))
            {
                element.GetComponent<SpriteRenderer>().color = Color.red;
                Ship.GetComponent<SpriteRenderer>().color = Color.red;
                anycollisions = true;
            }
            else
            {
                element.GetComponent<SpriteRenderer>().color = Color.white;
                if (anycollisions == false)
                {
                    Ship.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            //Debug.Log(dist);
            //Debug.Log(Erad + ShipRadius);
            //Debug.Log(Vector3.Distance(Ship.transform.position, current.transform.position));

        }


    }
    #endregion

    #region MethodSwitcher
    /// <summary>
    /// switches collision detection method
    /// </summary>
    /// <returns>boolean determining method used false = Circe, true = AABB</returns>
    bool keySwitcher()
    {
        bool swapMethod = true;
        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            swapMethod = true;
            //return true;
            Debug.Log("registered 1");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            swapMethod = false;
            //return false;
            Debug.Log("registered 2");
        }

        return swapMethod;
    }
    #endregion
}
