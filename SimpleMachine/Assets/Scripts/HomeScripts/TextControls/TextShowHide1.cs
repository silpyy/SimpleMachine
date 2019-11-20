using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShowHide : MonoBehaviour
{
    public void showText(GameObject textObjName)
    {
        //textObjName.getComponent<Renderer>().enable = true;
        textObjName.gameObject.GetComponent<UnityEngine.UI.Text>().enabled = true;
    }
    public void hideText(GameObject textObjName)
    {
        //Debug.Log(textObjName);
        textObjName.gameObject.GetComponent<UnityEngine.UI.Text>().enabled = false;
        //textObjName.getComponent<Renderer>().enable = false;
    }

   /* public IEnumerator BlinkText(GameObject textObjName)
    {
        showText(textObjName);
        yield return new WaitForSeconds(5);
        hideText(textObjName);
        yield return new WaitForSeconds(5);
        showText(textObjName);
    }*/
    /*public void BlinkText(GameObject textObjName)
    {
        showText(textObjName);
        yield return new WaitForSeconds(5);
        hideText(textObjName);
        //textObjName.getComponent<Renderer>().enable = false;
        //textObjName.gameObject.GetComponent<Renderer>().enabled = true;
        //yield return new WaitForSeconds(5);
        //textObjName.gameObject.GetComponent<Renderer>().enabled = false;
        *//*yield return new WaitForSeconds(5);
        textObjName.gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(5);
        textObjName.gameObject.GetComponent<Renderer>().enabled = false;*//*
    }*/
}
