using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class NavScripts : MonoBehaviour
{
    public GameObject navigationButtons;
    void Awake()
    {
        navigationButtons = GameObject.Find("NavButtons");
        //player = 
        //Debug.Log(navigationButtons);
        //navigationButtons.SetActive(false);
    }
    public void ShowHideNav(bool showNav)
    {
        if (showNav)
        {
            //Debug.Log("NavScript: " + showNav);
            navigationButtons.SetActive(true);
            
        }
        else
        {
            navigationButtons.SetActive(false);
        }

    }
}
