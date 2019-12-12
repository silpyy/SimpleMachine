using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private GameObject frontTyre;
    private GameObject backTyre;
    private int currentTyreNum = 0;
    [SerializeField] private WheelJoint2D fwheel;
    [SerializeField] private WheelJoint2D bwheel;
    private JointMotor2D fTyre;
    private JointMotor2D bTyre;
    private float carMoveForward;
    private Rigidbody2D _rb;

    private BoxCollider2D fTyreBoxCol;
    private BoxCollider2D bTyreBoxCol;
    private CircleCollider2D fTyreCircleCol; 
    private CircleCollider2D bTyreCircleCol;  
    private SpriteRenderer frontTtyreSprite;
    private SpriteRenderer backTtyreSprite;


    private float startTime;
    private float motorSpeed = 0f;
    private Text gearTxt;
    private Text SpeedText;
    private int gearVal = 1;
    private float vehicleSpeed;
    private bool driveVehicle = false;
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
        {"tyre_1", .00001f},
        {"tyre_2", .000015f},
        {"tyre_3", .01f}
    };
    private int tyreCount;

    //For healthbar
    [SerializeField] private HealthControl healthControl;
    private float haealthSize = 1f;
    private float healthDecVal;

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
        frontTtyreSprite = frontTyre.GetComponent<SpriteRenderer>();
        backTtyreSprite = backTyre.GetComponent<SpriteRenderer>();
        //Debug.Log(System.Math.Round(1.23456, 2));

        //Disabling boxcollider at first
        fTyreBoxCol = frontTyre.GetComponent<BoxCollider2D>();
        bTyreBoxCol = backTyre.GetComponent<BoxCollider2D>();
        fTyreCircleCol = frontTyre.GetComponent<CircleCollider2D>();
        bTyreCircleCol = backTyre.GetComponent<CircleCollider2D>();
        fTyreBoxCol.enabled = false;
        bTyreBoxCol.enabled = false;
        //gearTxt = GameObject.Find("Gear").GetComponent<Text>();
        SpeedText = GameObject.Find("Speed").GetComponent<Text>();
        //Debug.Log(gearTxt.text);
        //Set healthBar
        healthDecVal = healthDec["tyre_1"];
        //healthControl.SetSize(haealthSize);

        playControl = FindObjectOfType<PlayControls>();
    }

    void Update()
    {
        carMoveForward = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            startTime = Time.time;
            driveVehicle = true;
            Debug.Log("Drive");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            driveVehicle = false;
            Debug.Log("Don't Drive");
        }
        ManageMouseInputs(carMoveForward);

    }

    
    
    private void ManageMouseInputs(float moveForwards)
    {
        //Debug.Log("carSpeedVar" + carSpeedVar);
        // to move the car foreward/backward
        vehicleSpeedManager(currentTyreNum + 1, moveForwards);

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

    private void vehicleSpeedManager(int currentTyreNum, float for_Bck)
    {
        
        string tyreName = "tyre_" + currentTyreNum;
        //if(vehicleSpeed < 20)
        //{
        //Debug.Log("horizontal move val: " + for_Bck);
        if (driveVehicle)
        {
            if(for_Bck > 0)
            {
                if (vehicleSpeed < 300)
                {
                    motorSpeed += (float)((3.6 * 50 * fTyreCircleCol.radius) / (2f * 9.55f));
                    vehicleSpeed = motorSpeed / 10;
                    fTyre.maxMotorTorque = 100000;
                    bTyre.maxMotorTorque = 100000;
                    //Debug.Log(vehicleSpeed);
                }else if (vehicleSpeed < 1000)
                {
                    motorSpeed += (float)((3.6 * 100 * fTyreCircleCol.radius) / (2f * 9.55f));
                    vehicleSpeed = motorSpeed / 10;
                    fTyre.maxMotorTorque = 500;
                    bTyre.maxMotorTorque = 500;
                }
            }else if(for_Bck < 0)
            {
                if(vehicleSpeed > 0)
                {
                    motorSpeed = 0;
                    vehicleSpeed = motorSpeed / 10;
                }else if(vehicleSpeed >= -200)
                {
                    motorSpeed -= (float)((3.6 * 100 * fTyreCircleCol.radius) / (2f * 9.55f));
                    vehicleSpeed = motorSpeed / 10;
                }
            }

            HealthControl();
        }
        else
        {
            motorSpeed = vehicleSpeed * 10;
            if(vehicleSpeed != 0f)
            {
                if (vehicleSpeed > 0f)
                {
                    vehicleSpeed -= Time.deltaTime * 1000;
                }
                else if (vehicleSpeed < 0f)
                {
                    vehicleSpeed += Time.deltaTime * 750;
                }
            }
        }


        SpeedText.text = "Speed: "+(int)vehicleSpeed/10;
        //set motorspeed 
        fTyre.motorSpeed = vehicleSpeed;
        bTyre.motorSpeed = vehicleSpeed;
        fwheel.motor = fTyre;
        bwheel.motor = bTyre;
    }
    private void setNewTyre(int currentTyreNum, bool changeSpeed)  //changeSpeed -> if true changes the speed only and if false sets new tyres 
    {
        string tyreName = "tyre_" + currentTyreNum;
        if (!changeSpeed)
        {
            //set tyres
            frontTtyreSprite.sprite = Resources.Load<Sprite>("Images/CarSprites/" + tyreName);
            backTtyreSprite.sprite = Resources.Load<Sprite>("Images/CarSprites/" + tyreName);

            string colliderName = tyreColliderType[tyreName];

            manageTyreColliders(tyreName, colliderName); //manage the colliders according to tyreShapes
            healthDecVal = healthDec[tyreName];
            Debug.Log("Health decrease by: "+healthDecVal);
        }
        
    }

    private void manageTyreColliders(string tyreName, string colliderName)
    {

        if (colliderName == "circleCol")
        {
            fTyreCircleCol.enabled = true;
            bTyreCircleCol.enabled = true;
            fTyreBoxCol.enabled = false;
            bTyreBoxCol.enabled = false;

            fTyreCircleCol.radius = tyreColRadius[tyreName];
            bTyreCircleCol.radius = tyreColRadius[tyreName];
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


/*
if (timeDifTrack > 0f && timeDifTrack < 3f)
        {

            motorSpeed += (float) (Time.deltaTime * (3.6 * fTyre.maxMotorTorque * fTyreCircleCol.radius) / (2f * 9.55f) * 100);
            gearTxt.text = "Gear : 1";
            SpeedText.text = "SPeed: " + motorSpeed/100;
            fTyre.maxMotorTorque = 100;
            bTyre.maxMotorTorque = 100;
            Debug.Log("motorSpeed_1");
        }
        else if (timeDifTrack > 3f && timeDifTrack < 6f)
        {
            motorSpeed +=  2 * Time.deltaTime * 10;
            gearTxt.text = "Gear : 2";
            SpeedText.text = "SPeed: " + motorSpeed;
            fTyre.maxMotorTorque = 1000;
            bTyre.maxMotorTorque = 1000;
            Debug.Log("motorSpeed_2");
        }
        else if (timeDifTrack > 6f)
        {
            motorSpeed +=  3 * Time.deltaTime * 10;
            gearTxt.text = "Gear : 3";
            SpeedText.text = "SPeed: " + motorSpeed;
            fTyre.maxMotorTorque = 10000;
            bTyre.maxMotorTorque = 10000;
            Debug.Log("motorSpeed_3");
           // Debug.Log("timeDifTrack : "+ timeDifTrack);
        }
        else
        {
            motorSpeed = 0;
            fTyre.maxMotorTorque = 0;
            bTyre.maxMotorTorque = 0;
        }



    From manageMouseInput
    if (carSpeedVar > 0)
        {
           // _rb.velocity = new Vector2(carSpeedVar * 5 * Time.deltaTime, 0);
            SetSpeed(currentTyreNum + 1);
            HealthControl(); // decrease healthbar according to tyre and time
            timeDifTrack = Time.time - startTime;
            //Debug.Log("Time DIffernce: "+ timeDifTrack);
        }
        else if (carSpeedVar < 0)
        {
            //_rb.velocity = new Vector2(carSpeedVar * 5 * Time.deltaTime, 0);
            //SetSpeed(currentTyreNum + 1);
            HealthControl();
        }
        else
        {
            //Debug.Log(carSpeedVar);
            //SetSpeed(currentTyreNum + 1);
            gearTxt.text = "Gear : N";
            SpeedText.text = "SPeed: " + motorSpeed;
        }
 */
