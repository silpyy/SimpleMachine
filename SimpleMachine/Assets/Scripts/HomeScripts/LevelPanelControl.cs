using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelControl : MonoBehaviour
{
    private GameObject optnPanel;
    private Button button;

    void Start()
    {
        Debug.Log("call");
        optnPanel = GameObject.Find("LevelPanelContainer");
        //Debug.Log(optnPanel);
        optnPanel.transform.GetChild(0).gameObject.SetActive(false);
        button = optnPanel.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        button.interactable = false;
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
