using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    public LevelManager levManager;
    private ScoreCounter scorecounter;
    void Start()
    {
        levManager = FindObjectOfType<LevelManager>();
        scorecounter = FindObjectOfType<ScoreCounter>();
       //levManager.ShowScore(0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        string[] splitArr = this.gameObject.name.Split('_');
        //Debug.Log(splitArr[1]);
        int coinVal = int.Parse((splitArr[1].Split(' '))[0], System.Globalization.NumberStyles.Integer);
        scorecounter.SetSCore(coinVal);
        //int coinVal = Convert.ToInt32(splitArr[1]);
        if (other.tag == "Player")
        {
            //Debug.Log(coinVal);
            levManager.ShowScore(scorecounter.GetScore());
            Destroy(gameObject);
        }
    }
}
