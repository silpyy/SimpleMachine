using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Networking;

public class LoadXml : MonoBehaviour
{
    //public string dataPath = "/strings/";
    static string lang = "en";
    public string pathToData;
    public XmlDocument data = new XmlDocument();
    void Start()
    {
        TextAsset SourceFile = (TextAsset)Resources.Load("dataFiles/data-" + lang, typeof(TextAsset));
        XmlDocument xmldoc = new XmlDocument();
        Debug.Log(SourceFile);
        xmldoc.LoadXml(SourceFile.text);
        Debug.Log(xmldoc);
        Debug.Log("data: " + xmldoc.SelectNodes("/strings/string[@id='homeText']")[0].InnerText.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
