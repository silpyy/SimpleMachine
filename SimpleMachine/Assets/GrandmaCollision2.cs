using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GrandmaCollision2 : MonoBehaviour
{
    public GameObject choice1;
    public GameObject grandma;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "granny")
        {
            Debug.Log("here in grannny");
            choice1.SetActive(true);
            this.gameObject.SetActive(false);
            Time.timeScale = 0f;
            grandma.SetActive(false);
        }
    }
}
