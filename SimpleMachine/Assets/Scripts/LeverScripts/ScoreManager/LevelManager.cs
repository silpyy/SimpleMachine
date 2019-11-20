using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LevelManager : MonoBehaviour
{
    public LanguageController languagecontrol;
    private XmlDocument data = new XmlDocument();
    public Text coinText;
    public  int scoreVal = 0;
    private ScoreCounter scorecounter;

    private void Awake()
    {
        //Debug.Log(scoreVal);
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        languagecontrol = FindObjectOfType<LanguageController>();
        data = languagecontrol.DataFile();
        coinText = GameObject.Find("coinText")?.GetComponent<Text>();
        scorecounter = FindObjectOfType<ScoreCounter>();
        //Debug.Log(scorecounter.GetScore());
        coinText.text = data.SelectNodes("/strings/" + "string[@id='coinText']")[0].InnerText.ToString() + scorecounter.GetScore();
    }
    public void ShowScore(int score)
    {
        ////coinText = GameObject.Find("coinText")?.GetComponent<Text>();
        scoreVal = scorecounter.GetScore();
        //Debug.Log("coinScore -> "+ scoreVal);
        coinText.text = data.SelectNodes("/strings/" + "string[@id='coinText']")[0].InnerText.ToString() + (scoreVal);
    }
}
