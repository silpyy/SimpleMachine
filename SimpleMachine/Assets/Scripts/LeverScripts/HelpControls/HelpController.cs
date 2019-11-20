using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{
    private LanguageController languagecontrols;
    private XmlDocument data = new XmlDocument();
    private GameObject helpObject;
    private Text helpText;
    private Text helpbtnText;
    [SerializeField] private Sprite helpImage;
    private GameObject helpPanel;
    void Start()
    {
        languagecontrols = FindObjectOfType<LanguageController>();

        helpObject = GameObject.Find("TextCanvas/HelpObject/HelpPanelContainer");
        helpbtnText = GameObject.Find("helpText")?.GetComponent<Text>();
        helpText = GameObject.Find("TextCanvas/HelpObject/HelpPanelContainer/HelpPanel/HelpText")?.GetComponent<Text>();
        helpImage = GameObject.Find(helpObject+ "/HelpPanel/HelpImage")?.GetComponent<Image>().sprite;

        //ShowHelp("moveFulcrum");
        //helpPanel = this.gameObject.transform.GetChild(1).GetChild(0).gameObject;
        helpPanel = this.gameObject.transform.GetChild(0).gameObject;
        //Debug.Log(helpPanel.name);
        helpPanel.SetActive(false);

        //Debug.Log(languagecontrols);
        data = languagecontrols.DataFile();
        helpbtnText.text = data.SelectNodes("/strings/" + "string[@id='helpText']")[0].InnerText.ToString();
    }

    public void ShowHelp(string helpTextId)
    {
        Time.timeScale = 0f;
        helpPanel.SetActive(true);
        Animator animator = GameObject.Find("TextCanvas/HelpObject/HelpPanelContainer/HelpPanel/HelpImage").GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        //Debug.Log(data);
        string message = data.SelectNodes("/strings/" + "string[@id='"+ helpTextId + "']")[0].InnerText.ToString();
        helpText.text = message;
    }
    public void ShowHelp(string helpTextId, string imageName)
    {

        helpPanel.SetActive(true);
        //Debug.Log(data.SelectNodes("/strings/" + "string[@id='" + helpTextId + "']")[0].InnerText.ToString());
        //string message = data.SelectNodes("/strings/" + "string[@id='" + helpTextId + "']")[0].InnerText.ToString();
        string message = data.SelectNodes("/strings/" + "string[@id='" + helpTextId + "']")[0].InnerText.ToString();
        helpText.text = message;
        helpImage = Resources.Load<Sprite>("/Images/lever");
    }

    public void HideHelp()
    {
        Time.timeScale = 1f;
        helpPanel.SetActive(false);
    }
}
