using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControls : MonoBehaviour
{

    //Scene reload
    private LoadScene manageScene;
    public static bool hasOverturned = false;
    private void Start()
    {
        manageScene = FindObjectOfType<LoadScene>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered : " + collision.name);
        if (collision.name == "Map")
        {
            Debug.Log("Triggered_1");
            hasOverturned = true;
            ScenesControl("WheelRace");
        }
    }

    public void ScenesControl(string sceneName)
    {
        manageScene.changemenuscene(sceneName);
    }
}
