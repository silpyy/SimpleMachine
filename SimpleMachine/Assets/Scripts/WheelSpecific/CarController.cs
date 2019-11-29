using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private GameObject frontTyre;
    private GameObject backTyre;
    private int currentTyreNum = 0;
    [SerializeField] private WheelJoint2D fwheel;
    [SerializeField] private WheelJoint2D bwheel;
    private JointMotor2D fTyre;
    private JointMotor2D bTyre;
    private float carSpeedVar;
    private Rigidbody2D _rb;

    private BoxCollider2D fTyreBoxCol;
    private BoxCollider2D bTyreBoxCol;

    // dictionary <TyreName, motorSpeed for that tyre>
    private Dictionary<string, int> TyreSpecs = new Dictionary<string, int>() {
        { "tyre_1", 600},
        { "tyre_2", 500},
        { "tyre_3", 0}
    };
    private Dictionary<string, float> tyreColRadius = new Dictionary<string, float>()
    {
        {"tyre_1", 0.45f },
        {"tyre_2", 1f },
        {"tyre_3", .2f }
    };
    private Dictionary<string, string> tyreColliderType = new Dictionary<string, string>()
    {
        {"tyre_1", "circleCol"},
        {"tyre_2", "circleCol"},
        {"tyre_3", "boxCol"}
    };
    private Dictionary<string, float> healthDec = new Dictionary<string, float>()
    {
        {"tyre_1", .0001f},
        {"tyre_2", .0002f},
        {"tyre_3", .01f}
    };
    private int tyreCount;

    //For healthbar
    [SerializeField] private HealthControl healthControl;
    private float haealthSize = 1f;
    private float healthDecVal = .0001f;

    //Scene reload
    private PlayControls playControl;
    void Start()
    {
        tyreCount = TyreSpecs.Count;
        frontTyre = GameObject.Find("FrontTyre");
        backTyre = GameObject.Find("BackTyre");
        fTyre = frontTyre.GetComponent<WheelJoint2D>().motor;
        bTyre = backTyre.GetComponent<WheelJoint2D>().motor;
        _rb = GetComponent<Rigidbody2D>();
        //Debug.Log(System.Math.Round(1.23456, 2));

        //Disabling boxcollider at first
        fTyreBoxCol = frontTyre.GetComponent<BoxCollider2D>();
        bTyreBoxCol = backTyre.GetComponent<BoxCollider2D>();
        fTyreBoxCol.enabled = false;
        bTyreBoxCol.enabled = false;

        //Set healthBar
        //healthControl.SetSize(haealthSize);

        playControl = FindObjectOfType<PlayControls>();
    }

    void Update()
    {
        carSpeedVar = Input.GetAxis("Horizontal");
        ManageMouseInputs(carSpeedVar);
    }

    private void ManageMouseInputs(float carSpeedVar)
    {

        // to move the car foreward/backward
        if (carSpeedVar > 0)
        {
            _rb.velocity = new Vector2(carSpeedVar * 5 * Time.deltaTime, 0);
            setNewTyre(currentTyreNum + 1, true);
            HealthControl(); // decrease healthbar according to tyre and time
        }
        else if (carSpeedVar < 0)
        {
            _rb.velocity = new Vector2(carSpeedVar * 5 * Time.deltaTime, 0);
            setNewTyre(currentTyreNum + 1, true);
            HealthControl();
        }
        else
        {
            //Debug.Log(carSpeedVar);
            setNewTyre(currentTyreNum + 1, true);
        }

        //to change car tyres on mouse scroll
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyUp(KeyCode.UpArrow))
        {
            //Debug.Log("Mouse scroll up: " + Input.GetAxis("Mouse ScrollWheel"));
            if (currentTyreNum < tyreCount - 1)
            {
                currentTyreNum += 1;
                setNewTyre(currentTyreNum + 1, false);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.Log("Mouse scroll down: " + Input.GetAxis("Mouse ScrollWheel"));
            if (currentTyreNum > 0)
            {
                currentTyreNum -= 1;
                setNewTyre(currentTyreNum + 1, false);
            }
        }
    }

    private void setNewTyre(int currentTyreNum, bool changeSpeed)  //changeSpeed -> if true changes the speed only and if false sets new tyres 
    {
        string tyreName = "tyre_" + currentTyreNum;
        if (!changeSpeed)
        {
            //set tyres
            frontTyre.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/CarSprites/" + tyreName);
            backTyre.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/CarSprites/" + tyreName);

            string colliderName = tyreColliderType[tyreName];

            manageTyreColliders(tyreName, colliderName); //manage the colliders according to tyreShapes
            healthDecVal = healthDec[tyreName];
        }
        else
        {
            //set motorspeed 
            fTyre.motorSpeed = TyreSpecs[tyreName] * carSpeedVar;
            bTyre.motorSpeed = TyreSpecs[tyreName] * carSpeedVar;
            fwheel.motor = fTyre;
            bwheel.motor = bTyre;
        }
    }

    private void manageTyreColliders(string tyreName, string colliderName)
    {

        if (colliderName == "circleCol")
        {
            frontTyre.GetComponent<CircleCollider2D>().enabled = true;
            backTyre.GetComponent<CircleCollider2D>().enabled = true;
            fTyreBoxCol.enabled = false;
            bTyreBoxCol.enabled = false;

            frontTyre.GetComponent<CircleCollider2D>().radius = tyreColRadius[tyreName];
            backTyre.GetComponent<CircleCollider2D>().radius = tyreColRadius[tyreName];
        }
        else if (colliderName == "boxCol")
        {
            frontTyre.GetComponent<CircleCollider2D>().enabled = false;
            backTyre.GetComponent<CircleCollider2D>().enabled = false;
            fTyreBoxCol.enabled = true;
            bTyreBoxCol.enabled = true;
            
            //restrict the tyres from rotating
            fTyreBoxCol.transform.eulerAngles = new Vector3(0, 0, 0);
            bTyreBoxCol.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void HealthControl()
    {
        //Debug.Log(healthDecVal);
        haealthSize -= healthDecVal;
        if(haealthSize > 0)
        {
            healthControl.SetSize(haealthSize);
        }
        else
        {
            playControl.ScenesControl("WheelRace");
        }
    }

}


