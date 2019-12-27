using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelControl : MonoBehaviour
{
    private GameObject optnPanel;
    void Start()
    {
        optnPanel = GameObject.Find("LevelPanelContainer");
        optnPanel.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowObject()
    {
        optnPanel.transform.GetChild(0).gameObject.SetActive(true);
        //Debug.Log("show called");
    }

    public void HideObject()
    {
        optnPanel.transform.GetChild(0).gameObject.SetActive(false);
    }
}
