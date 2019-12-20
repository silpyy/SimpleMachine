using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControls : MonoBehaviour
{
    public PlayControls playcontrol;
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
        string[] nameArr = collision.name.Split(' ');
        Debug.Log(nameArr[0]);

        if (nameArr[0] == "Map")
        {
            Debug.Log("Triggered_1");
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
