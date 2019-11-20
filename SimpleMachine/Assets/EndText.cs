using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndText : MonoBehaviour
{
    public Text endText;
    // Update is called once per frame
    void start(){
        endText.text = "Thank you for dropping Ram home. Your score is " + CoinScript.exportingcoins;
    }
    void Update()
    {
        endText.text = "Thank you for dropping Ram home. Your score is " + CoinScript.exportingcoins;
    }
}
