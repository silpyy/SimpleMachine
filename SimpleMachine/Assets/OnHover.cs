//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
using UnityEngine;

public class OnHover : MonoBehaviour
{

    public GameObject ObjectToHide;
    public GameObject ClickOnHide;
    public GameObject player;
    public GameObject hangingpanel;
    public GameObject liftboypanel;
    public GameObject fallendown;
    public GameObject areaeffector;
    public GameObject pushareaeffector;
    public GameObject flag1;
    public GameObject grandmamsg;
    public GameObject jumpboy;
    public GameObject skateanimation;
    public GameObject skatenotice;
    public bool jumpclick = false;
    public bool jumpclick2 = false;
    public static bool skateactivate;
    //GameObject referenceObject;
      //GameObject referenceObject2;
      //GameObject referenceObject3;
       //CoinScript referenceScript;
       //CoinScript referenceScript2;
       //CoinScript referenceScript3;

    //private int flag=0;

    void Start()
    {
     ObjectToHide.SetActive(false);

       //referenceObject = GameObject.Find("coin1");
      // referenceObject2 = GameObject.Find("coin2");
       //referenceObject3 = GameObject.Find("coin3");
       //referenceScript = referenceObject.GetComponent<CoinScript>();
       //referenceScript2 = referenceObject2.GetComponent<CoinScript>();
       //referenceScript3 = referenceObject3.GetComponent<CoinScript>();
    }

    void OnMouseOver()
    {
            ObjectToHide.SetActive(true);
  
    }

    void OnMouseExit()
    {
        ObjectToHide.SetActive(false);
    }

    void OnMouseDown()
    {
        ObjectToHide.SetActive(true);
        ClickOnHide.SetActive(false);

        Debug.Log(this.gameObject.name);
        switch (this.gameObject.name)
        {
            case "plane1":
           // referenceScript.coinValue = 5;
            //flag=2;
            break;
            case "plane2":
                //referenceScript.coinValue = 3;
                grandmamsg.SetActive(true);
                Time.timeScale = 0f;
            //flag=2;
            break;
            case "plane3":
            //referenceScript.coinValue = 2;
            //flag=2;
            break;
            case "plane4":

                //referenceScript.coinValue = 1;
                //flag=2;
                break;

            case "plane1a":
                areaeffector.SetActive(true);
                Invoke("showfallendownmsg", 2.2f);
                Invoke("disableboxcollider", 1f);
                jumpclick = true;
                player.SetActive(false);
                //referenceScript2.coinValue = 5;
                
                break;
            case "plane2a":
            //referenceScript2.coinValue = 3;
            break;
            case "plane3a":
                pushareaeffector.SetActive(true);
                Invoke("showfallendownmsg2", 2f);
                jumpclick2 = true;
                Debug.Log(jumpclick2 + "from plane3a");
                skateanimation.GetComponent<Animator>().SetBool("jumpflag2", true);
                player.SetActive(false);
                //referenceScript2.coinValue = 2;
                break;
            case "plane4a":
            //referenceScript2.coinValue = 1;
            break;

            case "plane1b":
            //referenceScript3.coinValue = 5;
                flag1.SetActive(false);
            break;
            case "plane2b":
                //referenceScript3.coinValue = 3;
                liftboypanel.SetActive(true);
                player.SetActive(false);
                Time.timeScale = 0f;
                flag1.SetActive(false);

                break;
            case "plane3b":
            //referenceScript3.coinValue = 2;
                hangingpanel.SetActive(true);
                player.SetActive(false);
            Time.timeScale = 0f;
                flag1.SetActive(false);
                break;
            case "plane4b":
                Debug.Log(skateactivate);
                skateactivate = true;
                Debug.Log(skateactivate);
                player.SetActive(false);
                flag1.SetActive(false);
                break;

            default:
            break;
        }

        //referenceScript.coins=referenceScript3.coinValue;
        //if(flag==2){
       //     referenceScript2.coins=referenceScript.coins + referenceScript.coinValue;
      //  }

    }

    public bool skatestate()
    {
        Debug.Log("From return: "+skateactivate);
        return skateactivate;
    }
    private void showfallendownmsg()
    {
        fallendown.SetActive(true);
        Time.timeScale = 0f;
    }
    private void showfallendownmsg2()
    {
        skatenotice.SetActive(true);
        Time.timeScale = 0f;
    }
    private void disableboxcollider()
    {
        jumpboy.GetComponent<BoxCollider2D>().enabled = false;

    }


}