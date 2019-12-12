using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControls : MonoBehaviour
{

    //Scene reload
    private LoadScene manageScene;
    private LevelPanelControl _LPC;
    public static bool hasOverturned = false;
    public void Start()
    {
        manageScene = FindObjectOfType<LoadScene>();
        _LPC = FindObjectOfType<LevelPanelControl>();
        //Debug.Log(_LPC);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Triggered : " + collision.name);
        if (collision.name == "Map" || collision.name == "Map (1)" || collision.name == "platform-detail-01")
        {
            //Debug.Log("Triggered_1");
            hasOverturned = true;
            //_LPC.ShowObject();
            ScenesControl("WheelRace");
        }
    }

    public void ScenesControl(string sceneName)
    {
        _LPC.ShowObject();
        manageScene.changemenuscene(sceneName);
    }
}
