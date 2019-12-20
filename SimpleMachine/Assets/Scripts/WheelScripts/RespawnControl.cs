using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControl : MonoBehaviour
{
    public static Vector3 latestSpawnPoint;
    public static bool shouldRespwnInNewPos = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            latestSpawnPoint = this.transform.position;
            shouldRespwnInNewPos = true;
        }
    }
}
