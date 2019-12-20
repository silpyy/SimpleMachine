using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    //For healthbar
    [SerializeField] private HealthControl healthControl;
    private float haealthSize = 1f;
    private float healthDecVal;

    private AudioControls audioControl;
    private WheelJoint2D frontWheel;
    private WheelJoint2D backWheel;
    private void Start()
    {
        audioControl = FindObjectOfType<AudioControls>();
        //left to be refactored
        frontWheel = GameObject.Find("FrontTyre").GetComponent<WheelJoint2D>();
        backWheel = GameObject.Find("BackTyre").GetComponent<WheelJoint2D>();
        Debug.Log(audioControl);

        if (RespawnControl.shouldRespwnInNewPos)
        {
            transform.position = new Vector3(RespawnControl.latestSpawnPoint.x, 18f, 0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision of car");
        if (collision.collider.tag == "LandPlatform")
        {
            WheelController.hasCrashed = true;
            Debug.Log("car crashed");
            frontWheel.enabled = false;
            backWheel.enabled = false;
            audioControl.playAudios("crash");
        }
    }

    private void HealthControl()
    {
        //Debug.Log(healthDecVal);
        haealthSize -= healthDecVal;
        if (haealthSize > 0)
        {
            healthControl.SetSize(haealthSize);
        }
    }



}



