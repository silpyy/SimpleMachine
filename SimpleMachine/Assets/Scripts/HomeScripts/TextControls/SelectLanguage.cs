using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguage : MonoBehaviour
{
    public Dropdown dropdown;
    List<string> languages = new List<string>() { "En", "Np", "Sp" };
    public static string language = "en";
    public string selectedLAnguage;
    void Start()
    {
        dropdown = GameObject.Find("Dropdown")?.GetComponent<Dropdown>();
        if(dropdown != null)
        {
         PopulateDropdown();
        }
        selectedLAnguage = language;
    }

    void Update()
    {

        if((GameObject.Find("Dropdown")?.GetComponent<Dropdown>() != null) && languages[dropdown.value] != selectedLAnguage)
        {
            selectedLAnguage = languages[dropdown.value];
            language = selectedLAnguage;
            //Debug.Log("sel language: "+selectedLAnguage);
        }
    }

    void PopulateDropdown()
    {
        dropdown.options.Clear();
        dropdown.AddOptions(languages);
    }

    public string CurrrentLanguage()
    {
        //Debug.Log(language);
        return language.ToLower();
    }
}
