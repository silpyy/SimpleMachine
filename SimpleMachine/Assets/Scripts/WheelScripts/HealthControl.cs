using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    private Image bar;
    void Start()
    {
        bar = GameObject.Find("BarImg")?.GetComponent<Image>();
    }

    public void SetSize(float sizeNormalized)
    {
        //Debug.Log(sizeNormalized);
        bar.fillAmount = sizeNormalized;
    }
}
