using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    //For healthbar
    [SerializeField] private HealthControl healthControl;
    private float healthSize = 1;
    private float healthDecVal;

    private AudioControls audioControl;
    private WheelJoint2D frontWheel;
    private WheelJoint2D backWheel;
    private GameObject restartButton;
    private LevelNavControl levelNav;
    private void Start()
    {
        audioControl = FindObjectOfType<AudioControls>();
        levelNav = FindObjectOfType<LevelNavControl>();
        frontWheel = GameObject.Find("FrontTyre").GetComponent<WheelJoint2D>();
        backWheel = GameObject.Find("BackTyre").GetComponent<WheelJoint2D>();
        restartButton = GameObject.Find("RestartButton").transform.GetChild(0).gameObject;
        restartButton.SetActive(false);
        if (RespawnControl.shouldRespwnInNewPos)
        {
            transform.position = new Vector3(RespawnControl.latestSpawnPoint.x, 18f, 0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //destry and play audio if vehicle somesaults
        if (collision.collider.tag == "LandPlatform" && gameObject.tag == "KillerBIrd")
        {
            WheelController.hasCrashed = true;
            frontWheel.enabled = false;
            backWheel.enabled = false;
            audioControl.playAudios("crash");
            ShowRestartBtn();
        }
        else if(collision.collider.tag == "LandPlatform")
        {
            audioControl.playAudios("crash");
        }
    }

    public void ShowRestartBtn()
    {
        //restartButton.SetActive(true);
        //levelNav.ShowPanel();
        levelNav.ShowOptionsPanel("EndMsg", true, true, true);
    }
}



