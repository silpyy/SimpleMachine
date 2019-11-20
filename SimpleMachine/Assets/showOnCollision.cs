using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject choice1;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("heyheyhey");
            choice1.SetActive(true);
            Time.timeScale = 0f;
            this.gameObject.SetActive(false);

        }
    }
   
}
