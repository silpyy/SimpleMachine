using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class Lever1Controls : MonoBehaviour
{
    private PlayerControls playercontrol;
    private bool stopPlayerMovements = false;
    private GameObject player;
    private BoxCollider2D leveLength;
    private AreaEffector2D areaeffector;
    private bool groundTouchFlag;
    private bool activateFly = false;
   // private bool activateFly = false;
    public AfterFlightCollisionController afterflightCC;
    public weightController weightcontrol;
    private PlayerMovements playermovement;

    //public GameObject load;
    public Transform load;
    // Start is called before the first frame update
    private TextShowHide textshowhide;
    //private GameObject weightText;

    //for text message
    private MessageController messagecontrol;
    private int loadCount;
    void Start()
    {
        playermovement = FindObjectOfType<PlayerMovements>();
        playercontrol = FindObjectOfType<PlayerControls>();
        weightcontrol = FindObjectOfType<weightController>();

        //load = GameObject.Find("Load_1");//Lever_1
        loadCount = this.transform.parent.GetChild(2).childCount;
        for (var i = 0; i < loadCount; i++)
        {
            this.transform.parent.GetChild(2).GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
        }
        //load = this.transform.parent.GetChild(2).GetChild(1);
        //load.GetComponent<Renderer>().enabled = false;

        leveLength = GetComponent<BoxCollider2D>();
        areaeffector = GetComponent<AreaEffector2D>();
        afterflightCC = FindObjectOfType<AfterFlightCollisionController>();
        areaeffector.enabled = false;
        player = GameObject.Find("Player");
        //weightText = GameObject.Find("ClickWeightText");
        messagecontrol = FindObjectOfType<MessageController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("player position:"+ player.transform.position);
        /*groundTouchFlag = playercontrol.groundTouchFlag();
        Debug.Log("Ground flag: "+groundTouchFlag);*/
        //PlayerWalkJump();
        if (!PlayerMovements.stopPlayerMovements)
        {
            playermovement.PlayerWalkJump();
        }
        if (activateFly && !afterflightCC.LandedFlag())
        {
            Invoke("PlayerFly", .5f);
        }
        else if (afterflightCC.LandedFlag())
        {

            //Debug.Log("CAncellaaaaaaa lever_1");
            activateFly = false;
            PlayerMovements.playerFly = false;
            PlayerMovements.stopPlayerMovements = false;
            playermovement.PlayerWalkJump();
            //AfterFlightCollisionController.hasPlayerLanded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {

            PlayerMovements.stopPlayerMovements = true;
            PlayerMovements.playerSit = true;
            playermovement.PlayerSit();
            /*//test for text echoes
            Debug.Log("In controls: " + data.SelectNodes("/strings/" + "string[@id='ClickWeightText']")[0].InnerText.ToString());
            messageText.text = data.SelectNodes("/strings/" + "string[@id='ClickWeightText']")[0].InnerText.ToString();
            //test for text echoes*/
            messagecontrol.ShowMessage("ClickWeightText");
            //weightText.gameObject.GetComponent<UnityEngine.UI.Text>().enabled = true;

            player.transform.position = new Vector3(69f, player.transform.position.y, -1);
            //Show the loads
            for (var i = 0; i < loadCount; i++)
            {
                this.transform.parent.GetChild(2).GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
            }
            //load.GetComponent<Renderer>().enabled = true;
            //weightcontrol.weightClicked("Load_1");
            //weightcontrol.GetComponent<Renderer>().enabled = true;
        }
        if(collision.collider.tag == "Load")
        {
            areaeffector.enabled = true;
            string loadValue = collision.collider.name.Split('_')[2];
            ForceAndAngleManager(loadValue);
            PlayerMovements.playerSit = false;
            PlayerMovements.playerFly = true;
            activateFly = true;

            messagecontrol.ShowNoMessage();
            //weightText.gameObject.GetComponent<UnityEngine.UI.Text>().enabled = false;
            //messageText.text = "";
            //Invoke("PlayerFly", .1f);
            //PlayerFly();
        }
    }

    void ForceAndAngleManager(string loadId)
    {
        if (loadId == "1")
        {
            areaeffector.forceMagnitude = 3000;
        }
        else if (loadId == "2") {
            areaeffector.forceMagnitude = 4000;
        }else if(loadId == "3")
        {
            areaeffector.forceMagnitude = 4500;

        }

    }



    void PlayerFly()
    {
        //Debug.Log("Invoked");
        playermovement.PlayerFly();
    }






















    /*void PlayerSit()
    {
        stopPlayerMovements = true;
        playercontrol.PlayerSit(true);
        playercontrol.playerFly(false);
        playercontrol.PlayerWalk(false);
        playercontrol.PlayerJump(false);
    }
     void PlayerFly()
    {
        stopPlayerMovements = true;
        playercontrol.playerFly(true);
        playercontrol.PlayerWalk(false);
        playercontrol.PlayerJump(false);
        playercontrol.PlayerSit(false);
    }

    void PlayerWalkJump()
    {
        if (!stopPlayerMovements)
        {
            playercontrol.PlayerWalk(true);
            playercontrol.PlayerJump(true);
            playercontrol.PlayerSit(false);
            playercontrol.playerFly(false);
        }
    } */

}
