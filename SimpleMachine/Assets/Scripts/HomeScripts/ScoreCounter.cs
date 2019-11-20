using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    //Static variable
    public static int scoreCount = 0;
    void Start()
    {
        //SetSCore(10);
        //SetSCore(-5);
        //GetScore();
        scoreCount = 0;
    }
    public void ResetScore()
    {
        scoreCount = 0;
    }

    public void SetSCore(int value)
    {
        //Debug.Log(value);
        scoreCount += value;
    }
    public int GetScore()
    {
        //Debug.Log(scoreCount);
        return scoreCount;
    }
}
