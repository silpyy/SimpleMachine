﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class skateinformation : MonoBehaviour
{
    //test for text echoes
    private LanguageController languagecontrols;
    private XmlDocument data = new XmlDocument();
    private GameObject messageObject;
    private Text messageText;

    //test for text echoes
    void Start()
    {
        languagecontrols = FindObjectOfType<LanguageController>();
        data = languagecontrols.DataFile();
        messageObject = GameObject.Find("Canvas/skatepanel/Text");
        messageText = GameObject.Find("Canvas/skatepanel/Text")?.GetComponent<Text>();
        //    transform.GetChild(0).gameObject.SetActive (false);
        Invoke("ShowMessage", 0.1f);
        Invoke("hideObject", 0.11f);
    }

    public void ShowMessage()
    {
        string message = data.SelectNodes("/strings/" + "string[@id='text7']")[0].InnerText.ToString();
        messageText.text = message;
    }

    public void hideObject()
    {
        this.gameObject.SetActive(false);
    }



}