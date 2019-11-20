using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnbuttonposition : MonoBehaviour
{

    public respawnpoint1 respawnpoint;
    public GameObject Player;
    public respawnpoint1 respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        respawnpoint = FindObjectOfType<respawnpoint1>();
        Player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawnPlayer()
    {
        Player.transform.position = respawnpoint.GetRespawnPoint();
        Debug.Log(Player.transform.position + "     asdasdas" + respawnpoint.GetRespawnPoint());
    }
}
