using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightController : MonoBehaviour
{
    /*public PlayerMovementControls playercontrol;
    public GameObject clickTextObject;
    public TextShowHide textshowhide;*/
    private bool isLoadClicked = false;
    private string lastClickedobjectName;
    void Start()
    {
        /*playercontrol = FindObjectOfType<PlayerMovementControls>();
        playercontrol.isSitting();
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        textshowhide = FindObjectOfType<TextShowHide>();
        clickTextObject = GameObject.Find("ClickWeightText");*/
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
    void Update()
    {
        //Debug.Log(playercontrol.isSitting());
        /*if (playercontrol.isSitting())
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            if (!isLoadClicked)
            {
                textshowhide.showText(clickTextObject);
            }
            else
            {
                textshowhide.hideText(clickTextObject);
            }
        }*/
    }

     void OnMouseDown()
    {
        //Debug.Log("Load clicked");
        Rigidbody2D rigidBody = this.gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.mass = 10;
        isLoadClicked = true;
        lastClickedobjectName = this.gameObject.name;
        //Debug.Log(this.gameObject.name);
        int loadCount = this.transform.parent.childCount;
        // Debug.Log(this.transform.parent.childCount+" clicked name is"+ lastClickedobjectName);
        if (loadCount > 1)
        {
            for (var i = 0; i < loadCount; i++)
            {
                //Debug.Log("children Name"+ this.transform.parent.GetChild(i).name ++);
                if (this.transform.parent.GetChild(i).name != this.gameObject.name)
                {
                    this.transform.parent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }


    }

    public bool AddRigidBOdy()
    {
        return isLoadClicked;
    }
    public bool weightClicked(string objectName)
    {
        if(objectName == lastClickedobjectName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

/*
 * The see saw turns when it comes in contact with another object
 * So, to have the weightObject to have Gravity, RigidBody component must be added onClick which is given by 
 * --->Input.GetMouseButtonDown(0) 
 */
