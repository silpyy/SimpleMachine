using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesageBtn : MonoBehaviour
{
    private PlayerControls playerControl;
    private bool reload;

    private void Start()
    {

        playerControl = FindObjectOfType<PlayerControls>();
    }
    public void HidePanel()
    {
        reload = playerControl.IsReloaded();
        transform.parent.parent.GetChild(0).gameObject.SetActive(false);
        if (reload)
        {
            playerControl.KillPlayer();
        }
    }
}
