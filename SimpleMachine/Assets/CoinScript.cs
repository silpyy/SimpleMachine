using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public int coinValue;
    public Text coinText;
    public int coins;
    public static int exportingcoins;
    // Start is called before the first frame update
    void Start()
    {
         coinText.text = "Coins :"+ coins;
    }

    void OnTriggerEnter2D(Collider2D other){
        
         if (other.tag == "Player"){
             coins += coinValue;
             exportingcoins = coins;
             Debug.Log(exportingcoins);
             coinText.text = "Coins :"+ coins;
             Invoke("DestroyObject", .1f);
         }
    }

    void DestroyObject(){
             Destroy(gameObject);
    }
}
