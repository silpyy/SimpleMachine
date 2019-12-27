using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;


public class LevelNavControl : MonoBehaviour
{
    private GameObject levelPanel;
    //added later for levelPanel
    private GameObject optnPanel;
    private Text endText;
    private LanguageController languagecontrols;
    private XmlDocument data = new XmlDocument();
    private ScoreCounter scorecount;
    private Animator anim_1;
    private Animator anim_2;
    private Button level_1;
    private Button level_2;
    private Button level_3;

    private void Start()
    {
        //endText = GameObject.Find("TextCanvas/LevelPanelContainer/OptionsPanel/EndText")?.GetComponent<Text>();
        optnPanel = this.transform.GetChild(0).gameObject;
        languagecontrols = FindObjectOfType<LanguageController>();
        data = languagecontrols.DataFile();

        anim_1 = optnPanel.transform.GetChild(0).gameObject.GetComponent<Animator>();
        anim_2 = optnPanel.transform.GetChild(1).gameObject.GetComponent<Animator>();
        endText = optnPanel.transform.GetChild(4).gameObject.GetComponent<Text>();

        level_1 = optnPanel.transform.GetChild(0).gameObject.GetComponent<Button>();
        level_2 = optnPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
        level_3 = optnPanel.transform.GetChild(2).gameObject.GetComponent<Button>();
    }

    public void HidePanel()
    {
        optnPanel.SetActive(false);
    }

    public void ShowOptionsPanel(string endMessageId, bool activateLevel_1, bool activateLevel_2, bool activateLevel_3)
    {
        optnPanel.SetActive(true);
        endText.text = data.SelectNodes("/strings/" + "string[@id='"+endMessageId+ "']")[0].InnerText.ToString();
        level_1.interactable = activateLevel_1;
        level_2.interactable = activateLevel_2;
        level_3.interactable = activateLevel_3;

        Time.timeScale = 0f;
        anim_1.updateMode = AnimatorUpdateMode.UnscaledTime;
        anim_2.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}