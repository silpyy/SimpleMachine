using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetector : MonoBehaviour
{
    //public GameObject gameOver1;
    //public GameObject gameOver2;
    //public GameObject gameOver3;

    public LoadScene loadscene;

    private LevelPanelControl levelPanel;
    private GameObject optnPanel;
    private LevelNavControl levelNav;

    public void Start()
    {
        loadscene = FindObjectOfType<LoadScene>();
        levelPanel = FindObjectOfType<LevelPanelControl>();
        levelNav = FindObjectOfType<LevelNavControl>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            levelNav.ShowOptionsPanel("EndMsg", true, true, true);
            //Debug.Log("struck");
            //gameOver1.SetActive(true);
            //gameOver2.SetActive(true);
            // gameOver3.SetActive(true);

            //Added for testing to to load other scene and for level panel
            //loadscene.changemenuscene("SimpleMachinePrep");
            //optnPanel = GameObject.Find("LevelPanelContainer");
            //Animator animator1 = GameObject.Find("Canvas/LevelPanelContainer/OptionsPanel/LeverBtn").GetComponent<Animator>();
            //Debug.Log(optnPanel);
            //optnPanel.transform.GetChild(0).gameObject.SetActive(true);
            //optnPanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            //optnPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().interactable = false;
            //optnPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
            //optnPanel.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>().interactable = true;
            //Time.timeScale = 0f;
           // animator1.updateMode = AnimatorUpdateMode.UnscaledTime;
            //levelPanel.ShowObject();
        }
    }
}
