using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class lastpagetext : MonoBehaviour
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
       Debug.Log(data);
       messageObject = GameObject.Find("Canvas/LastPageInfo/Text");
       messageText = GameObject.Find("Canvas/LastPageInfo/Text")?.GetComponent<Text>();
        Invoke("ShowMessage", 0.1f);
        this.gameObject.SetActive(false);
   }

   public void ShowMessage()
   {
    string message = data.SelectNodes("/strings/" + "string[@id='text3']")[0].InnerText.ToString();
    messageText.text = message;
   }

   public void ShowNoMessage()
   {
    //    transform.GetChild(0).gameObject.SetActive(false);
   }
}