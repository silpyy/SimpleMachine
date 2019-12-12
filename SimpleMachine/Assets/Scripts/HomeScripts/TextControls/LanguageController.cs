using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    static string lang = "np";
    public Text langName;
    public SelectLanguage selectlanguage;
    public XmlNodeList stringList;
    public XmlDocument data = new XmlDocument();
    public string pathToData;
    private TextAsset SourceFile;

    void Start()
    {

        selectlanguage = FindObjectOfType<SelectLanguage>();
        lang = selectlanguage.CurrrentLanguage();
        SourceFile = (TextAsset)Resources.Load("dataFiles/data-" + lang, typeof(TextAsset));
        data.LoadXml(SourceFile.text);
        //Debug.Log(data);
        //Debug.Log("data: " + data.SelectNodes("/strings/string[@id='homeText']")[0].InnerText.ToString());
    }

    void Update()
    {
        if (selectlanguage.CurrrentLanguage() != lang)
        {
            loadXml();
            ChangeBtnTextsToCurLang(data);
        }
    }

    XmlDocument loadXml()
    {
        lang = selectlanguage.CurrrentLanguage();
        SourceFile = (TextAsset)Resources.Load("dataFiles/data-" + lang, typeof(TextAsset));
        Debug.Log("after language change: " + SourceFile);
        data.LoadXml(SourceFile.text);
        //Debug.Log("after language change: " + data);
        //Debug.Log("data: " + data.SelectNodes("/strings/string[@id='homeText']")[0].InnerText.ToString());
        return data;
    }

    /*
     * provide refernce to gameObject with texts.
     * make an array of those components.
     * import the xml file based on the language. The default language is english
     * Create a nodelist with particular id.
     * loop through the component array and echo the texts according to the nodeList value.
     * 
     * ******* Drawbacks of this approach********
     * Individual text component has to be referenced.
     * The order of gameObject components in the array and the order in the xml file must be the same. So, if the game is text rich,
       managing the gameObject components and xml file's ordering will be an issue. 
    */



    //this function is used for home scene------to be edited later

    //Comented for test
    public void ChangeBtnTextsToCurLang(XmlDocument data)
    {
        Text Start_btn = GameObject.Find("InclinedPlane_btn")?.GetComponent<Text>();
        Text Levels_btn = GameObject.Find("Lever_btn")?.GetComponent<Text>();

        Text[] componentsArray = new Text[] { Start_btn, Levels_btn};

        //get the list of nodes with the particular Id
        //XmlNodeList stringList = data.SelectNodes(dataPath + "string[@id='sm_txt']");
        stringList = data.SelectNodes("/strings/string[@id='sm_txt']");

        for (int i = 0; i < componentsArray.Length; i++)
        {
            //print(componentsArray[i]);
            if (componentsArray[i] != null)
            {
                componentsArray[i].text = stringList[i].InnerText.ToString();
            }
        }

    }
    //Comented for test

    public void TextObjectNames(List<string> objectNames, Text[] txtOBjects)
    {

        //Debug.Log("in language controller"+ objectNames);
        //Debug.Log("in language controller"+ txtOBjects);
        List<string> textObjList = new List<string>(objectNames);
        List<string> newStringList = new List<string>();
        //Debug.Log("ObjectName: " + txtOBjects[0].text);
        //Debug.Log(data.SelectNodes(dataPath + "string[@id='ClickWeightText']")[0].InnerText.ToString());
        for (int i = 0; i < txtOBjects.Length; i++)
        {

            if (txtOBjects[i] != null)
            {
                stringList = data.SelectNodes("/strings/string[@id='" + objectNames[i] + "']");
                newStringList.Add(stringList[0].InnerText.ToString());
                txtOBjects[i].text = newStringList[i];
                //Debug.Log("i: "+ i + " String value: "+newStringList[i]);
            }
            //Debug.Log(newStringList[i]);
        }
    }
    public XmlDocument DataFile()
    {
        //data.LoadXml(SourceFile.text);
        //Debug.Log(data);
        return (data);
    }





}
