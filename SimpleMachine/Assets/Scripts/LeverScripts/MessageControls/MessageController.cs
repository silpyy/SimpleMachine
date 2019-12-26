using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    //test for text echoes
    private LanguageController languagecontrols;
    private XmlDocument data = new XmlDocument();
    private GameObject messageObject;
    private Text messageText;
    private Text okayText;
    //test for text echoes

    private bool showHelpWithImg = false;
    void Start()
    {
        languagecontrols = FindObjectOfType<LanguageController>();
        data = languagecontrols.DataFile();
        messageObject = GameObject.Find("TextCanvas/MessageObject/Panel/Message");
        messageText = GameObject.Find("TextCanvas/MessageObject/Panel/Text")?.GetComponent<Text>();
        okayText = GameObject.Find("TextCanvas/MessageObject/Panel/OkayBtn/Text")?.GetComponent<Text>();
        //Debug.Log("Message Object: " + messageText);
        //okayText.text = data.SelectNodes("/strings/" + "string[@id='okay']")[0].InnerText.ToString();
        //to hide its child components and show only when necessary
        transform.GetChild(0).gameObject.SetActive (false);
        transform.GetChild(0).GetChild(3).gameObject.SetActive (false);
        transform.GetChild(0).GetChild(4).gameObject.SetActive (false);
    }

    public void ShowMessage(string messageId)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        string message = data.SelectNodes("/strings/" + "string[@id='"+ messageId + "']")[0].InnerText.ToString();
        messageText.text = message;
        if (showHelpWithImg)
        {
            transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
    }


    //show the message with the image in the message section
    public void ShowMessage(string messageId, bool showImg)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        string message = data.SelectNodes("/strings/" + "string[@id='" + messageId + "']")[0].InnerText.ToString();
        messageText.text = message;
        transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        showHelpWithImg = true;
    }



    //show the message with the image in the message section
    public void ShowMessage(string messageId, bool showOKBtn, string okayTextId)
    {
        //Debug.Log(messageId);
        transform.GetChild(0).gameObject.SetActive(true);
        //Debug.Log(data.SelectNodes("/strings/" + "string[@id='sqr_wheel']")[0].InnerText.ToString());
        string message = data.SelectNodes("/strings/" + "string[@id='" + messageId + "']")[0].InnerText.ToString();
        //okayText.text = data.SelectNodes("/strings/" + "string[@id='okay']")[0].InnerText.ToString();
        okayText.text = data.SelectNodes("/strings/" + "string[@id='"+ okayTextId + "']")[0].InnerText.ToString();
        messageText.text = message;
        transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
    }

    //message that vanishes after sometimes
    public void ShowVanishingMessage(string messageId)
    {
        transform.GetChild(0).GetChild(4).gameObject.SetActive(false); // hide the okay button
        transform.GetChild(0).gameObject.SetActive(true);
        string message = data.SelectNodes("/strings/" + "string[@id='" + messageId + "']")[0].InnerText.ToString();
        //okayText.text = data.SelectNodes("/strings/" + "string[@id='" + okayTextId + "']")[0].InnerText.ToString();
        messageText.text = message;
        Invoke("ShowNoMessage", 3f);
    }

    public void ShowNoMessage()
    {
        messageText.text = "";
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
