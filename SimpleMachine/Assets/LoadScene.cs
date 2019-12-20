using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{

    public void changemenuscene(string scenename) {
        //Debug.Log(scenename);
            SceneManager.LoadScene(scenename);
            Time.timeScale = 1f;
        // Whatever you want it to do.
    }
}
