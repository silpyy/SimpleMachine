using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lever2Controls : MonoBehaviour
{
    private PlayerControls playercontrol;
    private bool stopPlayerMovements = false;
    private GameObject player;
    private BoxCollider2D leveLength;
    private AreaEffector2D areaeffector;
    private bool groundTouchFlag;
    private static bool activateFly = false;
    public AfterFlightCollisionController afterflightCC;
    public weightController weightcontrol;
    public GameObject load;
    private PlayerMovements playermovement;
    public bool weightClick = false;
    private int hingeCallCount = 0;
    //private PlayerProperties playerProps;
    // 

    public float connectedAnchorPosition;
    public HingeJoint2D seeSawHinge;
    private float anchor_x;
    private float anchor_y;
    private float anchor_new_x;
    private float anchor_new_y;
    public GameObject fulcrumObj;
    private float fulcrum_Start_x;
    private float fulcrum_x;
    private float fulcrum_size;
    private float fulcrum_center_point;
    private float fulcrum_new_x;
    private Collider2D slab;
    private float slabWidth;
    private float slabSize;
    private float slab_x;
    //
    //for text message
    private MessageController messagecontrol;
    void Start()
    {
        //Debug.Log("Start called");
        slab = GetComponent<BoxCollider2D>();
        playercontrol = FindObjectOfType<PlayerControls>();
        playermovement = FindObjectOfType<PlayerMovements>();
        weightcontrol = FindObjectOfType<weightController>();
        leveLength = GetComponent<BoxCollider2D>();
        areaeffector = GetComponent<AreaEffector2D>();
        afterflightCC = FindObjectOfType<AfterFlightCollisionController>();
        //load = GameObject.Find("Loads/Load_2");
        load = this.transform.parent.GetChild(2).GetChild(0).gameObject;
        load.GetComponent<Renderer>().enabled = false;
        areaeffector.enabled = false;
        player = GameObject.Find("Player");
        seeSawHinge = GetComponent<HingeJoint2D>();
        seeSawHinge.enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        //

        slabWidth = GetComponent<BoxCollider2D>().size.x;
        //Debug.Log("slabWidth" + slabWidth );
        fulcrumObj = GameObject.FindWithTag("Fulcrum");
        fulcrum_Start_x = fulcrumObj.transform.position.x;
        //Debug.Log("fulcrum at start: " + fulcrum_Start_x);
        anchor_y = seeSawHinge.anchor.y;

        messagecontrol = FindObjectOfType<MessageController>();
        //Reset the static variable as flight control is dependent on this variable and if not reset gives issues with sound
        AfterFlightCollisionController.hasPlayerLanded = false;
        activateFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("position:"+ transform.position);
        //Debug.Log("player position:"+ player.transform.position);
        if (!PlayerMovements.stopPlayerMovements)
        {
            playermovement.PlayerWalkJump();
        }
        if (activateFly && !afterflightCC.LandedFlag())
        {
            Invoke("PlayerFly", .5f);
            //playermovement.PlayerFly();
        }
        else if (afterflightCC.LandedFlag())
        {
            activateFly = false;

            PlayerMovements.playerFly = false;
            PlayerMovements.stopPlayerMovements = false;
            playermovement.PlayerWalkJump();

            //AfterFlightCollisionController.hasPlayerLanded = false;
        }
        // flag to add hinge in lever when clicked only as addition of hinge at first, hampers dargging of fulcrum
        weightClick = weightcontrol.weightClicked("Load_2");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //Debug.Log("PLayer collided with lever!!!!");
            PlayerMovements.stopPlayerMovements = true;

            PlayerMovements.stopPlayerMovements = true;
            PlayerMovements.playerSit = true;
            playermovement.PlayerSit();
            AfterFlightCollisionController.hasPlayerLanded = false;
            //show move fulcrum message
            messagecontrol.ShowMessage("moveFulcrum", true);

            player.transform.position = new Vector3(214f, player.transform.position.y, -1);

            load.GetComponent<Renderer>().enabled = true;

            //Debug.Log("WC: "+weightClick);
            
        }
        if (collision.collider.tag == "Load")
        {
            //Debug.Log("WC: enter" + weightClick);
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            seeSawHinge.enabled = true;
            if(hingeCallCount < 1)
            {
                HingeFunctionality();
                hingeCallCount += 1;
            }
            
            //Debug.Log("fulcrum at last: "+fulcrumObj.transform.position.x);
            float fulcrumPosOffset = fulcrumObj.transform.position.x - fulcrum_Start_x;
            //Debug.Log(fulcrumPosOffset);
            areaeffector.enabled = true;
            ForceAndAngleManager(fulcrumPosOffset);

            PlayerMovements.playerSit = false;
            PlayerMovements.playerFly = true;
            //Debug.Log("activateFly set");
            activateFly = true;
            //show move fulcrum message
            messagecontrol.ShowNoMessage();
        }
    }

    void HingeFunctionality()
    {
        //Debug.Log("HingeFunctionality");
        //To find x axis of slab
        //As the slabs x position started from the left with the width half of slab size so 
        //subtracted the half of slab size from the x of slab
        //Dont know why need to look into this
        slabSize = slab.bounds.size.x;
        slab_x = transform.position.x;
        //slab_x = transform.position.x;
        //Debug.Log("Slab width = " + slabSize + "\n Slab x = " + slab_x);

        //find position axis of hinge
        anchor_x = seeSawHinge.anchor.x;
        //anchor_y = seeSawHinge.anchor.y;
        //Debug.Log("anchor_x = " + anchor_x + "\n anchor_y  = " + anchor_y);

        //find the x axis of the fulcrum which will act as the pivot.
        fulcrum_center_point = slab_x + (slabSize / 2);
        //Debug.Log("fulcrum_center_point = " + fulcrum_center_point);
        fulcrum_x = (fulcrumObj.transform.position.x / slab_x) * fulcrum_center_point;
        //Debug.Log("Fulcrum  x: "+ fulcrum_x +" \n fulcrum center point: "+ fulcrum_center_point);

        if (fulcrum_x < fulcrum_center_point || fulcrum_x > fulcrum_center_point)
        {
            anchor_new_x = 0;
            //fulcrum_x < fulcrum_center_point ? Debug.Log("less than satisfied") : Debug.Log("greater than satisfied");
            fulcrum_new_x = ((fulcrum_x - fulcrum_center_point) / (slabSize / 2)) * (slabWidth / 2);
            //Debug.Log("fulcrum_new_x"+ fulcrum_new_x);
            anchor_new_x = anchor_x + fulcrum_new_x;
        }
        else
        {
            anchor_new_x = 0;
        }

        //Debug.Log("New Anchor: x: " + anchor_new_x + " y-" + anchor_y);
        seeSawHinge.anchor = new Vector2(anchor_new_x, anchor_y);
    }
    void ForceAndAngleManager(float fulcrumPosOffset)
    {
        //Debug.Log("fulcrumPosOffset " + fulcrumPosOffset);
        if (fulcrumPosOffset > 0.7)
        {
            //Debug.Log("Angle manager gt: " + fulcrumPosOffset);
            //areaeffector.forceAngle = 20;
            int changeMagnitude = (int)fulcrumPosOffset;
            //Debug.Log("PositionChange: "+changeMagnitude);
            
            if (changeMagnitude > 1)
            {
                areaeffector.forceMagnitude = 450;
            }
            else
            {
                areaeffector.forceMagnitude = 400;
            }
            //areaeffector.forceAngle -= changeMagnitude * 9f;
            //areaeffector.forceAngle = changeMagnitude * 1f;
            /*if (areaeffector.forceAngle == 26)
            {
                areaeffector.forceMagnitude += (changeMagnitude * 8f);
            }
            else
            {
                areaeffector.forceMagnitude += (changeMagnitude * 7f);
            }*/

        }
        else if (fulcrumPosOffset < 0)
        {
            int  changeMagnitude = (int)fulcrumPosOffset;
            if(changeMagnitude > -2)
            {
                areaeffector.forceAngle = 40;
                areaeffector.forceMagnitude = 700;
            }
            else
            {
                areaeffector.forceAngle = 40;
                areaeffector.forceMagnitude = 900;
            }
            /*areaeffector.forceAngle -= changeMagnitude * 2f;
            areaeffector.forceMagnitude -= (changeMagnitude * 25f); */
            //areaeffector.forceMagnitude += changeMagnitude;
        }
        else
        {
            //Debug.Log("Angle manager eqv" + fulcrumPosOffset);
            areaeffector.forceAngle = 30;
            areaeffector.forceMagnitude = 425;
            //areaeffector.forceMagnitude = areaeffector.forceMagnitude;
        }

    }

    void PlayerFly()
    {
        //Debug.Log("Invoked");
        playermovement.PlayerFly();
    }

}
 
/*
 * this script is supposed to make the lever move based on the pivot of the fulcrum
 * Algorithm (used):
 *  find the x axis of the slab.
 *  find the x axis of the fulcrum which will act as the pivot.
 *  In hinge joint, rotation of the object depends on the anchor property of hinge component
 *  So, find x axis of hinge as well
 *  Find the centre point of the slab, above this the values will be +ve, and -ve below and will be zero at this point.
 *  To find new pivot or x-value of anchor:
 *      ((fulcrum's x - slab's center point)/(half of slab's size)) * size of box collider 
 *      this formula will give the value of pivot in +ve or -ve.
 *      set the anchor's x according to that.
******************************************************************************************
************************ TO DOS ***************
*  Understand why -- the slabs x position started from the left with the width half of slab size   
*/





