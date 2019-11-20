using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnpoint1 : MonoBehaviour
{
    // Static variable
    public static Vector2 respawnPointPosition;
    public static bool isRespawnReached = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isRespawnReached = true;
            respawnPointPosition = this.transform.position;
            Debug.Log("respawnPointPosition: "+ respawnPointPosition);
            //Debug.Log(this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("flag_green", typeof(Sprite)) as Sprite;
        }
    }
    public Vector2 GetRespawnPoint()
    {
        return respawnPointPosition;

    }

    public bool RespawnState()
    {
        return isRespawnReached;
    }
}