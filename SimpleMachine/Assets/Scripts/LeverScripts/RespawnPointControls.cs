using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointControls : MonoBehaviour
{
    // Static variable
    public static Vector2 respawnPointPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            respawnPointPosition = this.transform.position;
            //Debug.Log("respawnPointPosition: "+ respawnPointPosition);
            //Debug.Log(this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load("flag_green", typeof(Sprite)) as Sprite;
        }
    }
    public Vector2 GetRespawnPoint()
    {
        return respawnPointPosition;
    }
}
