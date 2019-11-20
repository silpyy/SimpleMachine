using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBtnClickControl : MonoBehaviour
{
    private HelpController helpControls;
    [SerializeField] private string helpTextId;
    void Start()
    {
        helpControls = FindObjectOfType<HelpController>();
    }

    public void ShowHelp()
    {
        helpControls.ShowHelp("lever_help");
    }

    public void HideHelp()
    {
        helpControls.HideHelp();
    }
}
