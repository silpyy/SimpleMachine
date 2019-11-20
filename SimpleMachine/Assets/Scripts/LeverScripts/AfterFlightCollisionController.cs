using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterFlightCollisionController : MonoBehaviour
{
    public static bool hasPlayerLanded = false;
    //public bool hasPlayerLanded = false;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (col.collider.tag == "Player")
        {
            //Debug.Log("afcc collider: "+ col.collider.tag);
            hasPlayerLanded = true;
        }
    }

    public bool  LandedFlag()
    {
        return hasPlayerLanded;
    }
}
