using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LoadTexts_Lever : MonoBehaviour
{
    public LanguageController languagecontrol;
    private XmlDocument data = new XmlDocument();
    public List<string> objectNames;
    // Start is called before the first frame update
    public Text[] componentsArray;
    public NavScripts navigation;
    public TextShowHide textshowhide;

    private int loadCount;
    void Start()
    {
        languagecontrol = FindObjectOfType<LanguageController>();
        data = languagecontrol.DataFile();
        navigation = FindObjectOfType<NavScripts>();
        textshowhide = FindObjectOfType<TextShowHide>();

        //GameObject clickTextObject = GameObject.Find("ClickWeightText");
        //Text ClickWeightText = GameObject.Find("ClickWeightText")?.GetComponent<Text>();
        Text homeText = GameObject.Find("homeText")?.GetComponent<Text>();
        Text replayText = GameObject.Find("replayText")?.GetComponent<Text>();
        Text helpText = GameObject.Find("helpText")?.GetComponent<Text>();
        //Debug.Log(helpText);
        // Debug.Log("childCount: "+GameObject.Find("NavButtons")?.GetComponent<Transform>().childCount);

        //componentsArray = new Text[] {ClickWeightText, homeText, replayText };
        componentsArray = new Text[] {homeText, replayText, helpText };

        string[] objNames = {"homeText", "replayText", "helpText" };

        objectNames = new List<string>(objNames);

        homeText.text = data.SelectNodes("/strings/" + "string[@id='homeText']")[0].InnerText.ToString();
        replayText.text = data.SelectNodes("/strings/" + "string[@id='replayText']")[0].InnerText.ToString();

        // textshowhide.hideText(clickTextObject);
        navigation.ShowHideNav(false);
    }

    // Update is called once per frame
    void Update()
    {
        //languagecontrol.TextObjectNames(objectNames, componentsArray);
    }
}
