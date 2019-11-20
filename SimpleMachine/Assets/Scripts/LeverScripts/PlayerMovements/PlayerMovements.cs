using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerControls playercontrol;
    public static bool stopPlayerMovements = false;
    public static bool playerFly = false;
    public static bool playerSit= false;

    private void Start()
    {
        playercontrol = FindObjectOfType<PlayerControls>();
    }
    private void Update()
    {
        //Debug.Log(stopPlayerMovements);
    }
    public void PlayerSit()
    {
        if (playerSit)
        {
            //Debug.Log("player sit");
            //stopPlayerMovements = true;
            playercontrol.PlayerSit(true);
            playercontrol.playerFly(false);
            playercontrol.PlayerWalk(false);
            playercontrol.PlayerJump(false);
        }
    }
    public void PlayerFly()    {
        if (playerFly)
        {
            //Debug.Log("player fly");
            playercontrol.playerFly(true);
            playercontrol.PlayerWalk(false);
            playercontrol.PlayerJump(false);
            playercontrol.PlayerSit(false);
        }
    }

    public void PlayerWalkJump()
    {
        //Debug.Log("player wlk jump");
        if (!stopPlayerMovements)
        {
            playercontrol.PlayerWalk(true);
            playercontrol.PlayerJump(true);
            playercontrol.PlayerSit(false);
            playercontrol.playerFly(false);
        }
    }
}
