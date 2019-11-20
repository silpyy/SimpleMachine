using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public SelectLanguage selectlanguage;
    private string currentLang;
    private GameObject backGround;
    void Start()
    {
        //Debug.Log("Background Initiated" + currentLang);
        selectlanguage = FindObjectOfType<SelectLanguage>();
        currentLang = selectlanguage.CurrrentLanguage();
        backGround = GameObject.Find("BackGround");
        // Debug.Log("Background Initiated"+ currentLang);
        SetUpBackground();
    }
    private void SetUpBackground()
    {
        int bgObjectNum = this.transform.childCount;
        Transform forest = this.transform.GetChild(0).GetChild(1).GetChild(0);
        //Debug.Log(this.transform.GetChild(0).name);
        Sprite spanishBg = Resources.Load("background_layer_10", typeof(Sprite)) as Sprite;
        //Debug.Log(forest.GetComponent<SpriteRenderer>().sprite);
        //Debug.Log(spanishBg);
        if (currentLang == "sp")
        {
            //Debug.Log("Spanish Version");
            //forest.GetComponent<SpriteRenderer>().sprite = spanishBg; 
            for(var i=0; i< (bgObjectNum-1); i++)
            {
                //Debug.Log(this.transform.GetChild(i).GetChild(1).name);
                this.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = spanishBg;
                this.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = spanishBg;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.childCount);
    }
}
