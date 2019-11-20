using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectiondetector : MonoBehaviour
{
    public GameObject toShow;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Time.timeScale = 0f;
            toShow.SetActive(true);
            // LastPageInfo.SetActive(true);
        }
    }
}
