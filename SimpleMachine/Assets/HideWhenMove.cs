using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWhenMove : MonoBehaviour
{

    void Update(){
         if (Input.anyKey)
        {
           this.gameObject.SetActive(false);
        }
    }
  
}
