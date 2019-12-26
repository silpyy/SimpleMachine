using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    private GameObject frontTyre;
    private GameObject backTyre;
    [SerializeField] private GameObject hill;
    private int currentTyreNum = 0;
    [SerializeField] private WheelJoint2D wheelJoint;
    private JointMotor2D wheelMotor;

    //private BoxCollider2D wheelBoxCollider;
    private CircleCollider2D wheelCircleCollider;
    private EdgeCollider2D slipperyHillCollider;

    private SpriteRenderer wheelSprite;

    private PhysicsMaterial2D slipperyMaterial;
    private PhysicsMaterial2D notSlipperyMaterial;

    private Dictionary<string, int> TyreSpecs = new Dictionary<string, int>() {
        { "tyre_1", 1200},
        { "tyre_2", 1000},
        { "tyre_3", 200},
        { "tyre_4", 100}
    };
    private Dictionary<string, float> tyreColRadius = new Dictionary<string, float>()
    {
        {"tyre_1", 1.6f },
        {"tyre_2", 3.1f },
        {"tyre_3", .2f },
        {"tyre_4", .2f }
    };
    private Dictionary<string, string> tyreColliderType = new Dictionary<string, string>()
    {
        {"tyre_1", "circleCol"},
        {"tyre_2", "circleCol"},
        {"tyre_3", "polygonCol"},
        {"tyre_4", "polygonCol"}
    };
    private Dictionary<string, float> healthDecStat = new Dictionary<string, float>()
    {
        {"tyre_1", .00001f},
        {"tyre_2", .00002f},
        {"tyre_3", .001f},
        {"tyre_4", .001f}
    };

    private int tyreCount;
    private float speedControlVar;
    private float speed = 1000f;
    public float currentheathDecVal;

    public AudioControls audioControl; 
    public static bool hasCrashed = false;


    private MessageController messageControls;
    private bool isMessageOn = false;
    public float healthSize = 1;
    private HealthControl healthControl;


    private void Start()
    {
        tyreCount = TyreSpecs.Count;
        wheelJoint = gameObject.GetComponent<WheelJoint2D>();
        wheelMotor = gameObject.GetComponent<WheelJoint2D>().motor;
        wheelSprite = gameObject.GetComponent<SpriteRenderer>();
        wheelCircleCollider = gameObject.GetComponent<CircleCollider2D>();
        //wheelBoxCollider = gameObject.GetComponent<BoxCollider2D>();
        //wheelBoxCollider.enabled = false;


        healthControl = FindObjectOfType<HealthControl>();
        //hill = GameObject.Find("Map (1)");
        slipperyHillCollider = hill.GetComponent<EdgeCollider2D>();

        notSlipperyMaterial = (PhysicsMaterial2D)Resources.Load("Materials/HillNotSlippery");
        slipperyMaterial = (PhysicsMaterial2D)Resources.Load("Materials/SlipperyHill");
        audioControl = FindObjectOfType<AudioControls>();
        messageControls = FindObjectOfType<MessageController>();
        hasCrashed = false;

        currentheathDecVal = (float)healthDecStat["tyre_" + (currentTyreNum+1)];
    }

    private void Update()
    {
        speedControlVar = Input.GetAxis("Horizontal");
        if (!hasCrashed)
        {
            //change the tyres on mouse scroll or on up/down arrow click
            ChangeWheelOnScroll();
            //set the speed of wheel motor according to arrow clicks
            SetMotorSpeed();
        }
        else
        {
            wheelMotor.motorSpeed = 0;
            wheelJoint.motor = wheelMotor;
        }

        if (!isMessageOn)
        {
            messageControls.ShowMessage("wheelIntro", true, "okay_wheel");
            isMessageOn = true;
        }

    }

    //set the motor speed that makes the vehicle move
    private void SetMotorSpeed()
    {
        if(speedControlVar > 0)
        {
            audioControl.playAudios("forward");
            speed = speed < 0 ? speed + (float)((3.6 * 200 * 2) / (2f * 9.55f)) :
                    speed < 2200 ? speed + (float)((3.6 * 25 * 2) / (2f * 9.55f)) :
                    2200;
            HealthControl();
            HideFirstMessage();
        }
        else if(speedControlVar < 0)
        {
            audioControl.playAudios("backward");
            speed = speed > 0 ? speed - (float)((3.6 * 200 * 2) / (2f * 9.55f)) :
                    speed > -1500 ? speed - (float)((3.6 * 25 * 2) / (2f * 9.55f)) :
                    -1500;
            HealthControl();
            HideFirstMessage();
        }
        else
        {
             speed = speed > 100 ? speed - (float)((3.6 * 50 * 2) / (2f * 9.55f)) :
                     speed < -100 ? speed + (float)((3.6 * 50 * 2) / (2f * 9.55f)) :
                     0;
             audioControl.StopAudio();
        }
        wheelMotor.motorSpeed = speed;
        wheelJoint.motor = wheelMotor;
        GameObject.Find("Speed").GetComponent<Text>().text = "" + speed;

    }
   

    //Load and set new tyres on up arrows or mouse scroll
    private void ChangeWheelOnScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (currentTyreNum < tyreCount - 1)
            {
                currentTyreNum += 1;
                setNewTyre(currentTyreNum + 1);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (currentTyreNum > 0)
            {
                currentTyreNum -= 1;
                setNewTyre(currentTyreNum + 1);
            }
        }
    }


    private void setNewTyre(int currentTyreNum) 
    {
        string tyreName = "tyre_" + currentTyreNum;
        wheelSprite.sprite = Resources.Load<Sprite>("Images/CarSprites/" + tyreName);
        string colliderName = tyreColliderType[tyreName];
        manageTyreColliders(tyreName, colliderName);
        ManageMaterials(currentTyreNum);
        currentheathDecVal = (float)healthDecStat["tyre_" + currentTyreNum];
        if (currentTyreNum > 2)
        {
            //messageControls.ShowMessage("sqr_wheel", true, "okay_wheel");
            messageControls.ShowVanishingMessage("sqr_wheel");
        }
    }

    
    //set material for the slippery road
    private void ManageMaterials(int currentTyreNum)
    {
        Debug.Log(currentTyreNum);
        switch (currentTyreNum)
        {
            case 2:
                slipperyHillCollider.sharedMaterial = notSlipperyMaterial;
                break;
            default:
                slipperyHillCollider.sharedMaterial = slipperyMaterial;
                break;
        }
    }


    //set colliders according to the changig tyres
    private void manageTyreColliders(string tyreName, string colliderName)
    {
        switch (colliderName)
        {
            case "circleCol":

                wheelCircleCollider.enabled = true;
                //wheelBoxCollider.enabled = false;
                var polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    Destroy(polygonCollider);
                }

                wheelCircleCollider.radius = tyreColRadius[tyreName];
               // messageControls.ShowNoMessage();
                break;
            case "polygonCol":

                //messageControls.ShowMessage("sqr_wheel", false, true, "okay_wheel");

                wheelCircleCollider.enabled = false;
                //wheelBoxCollider.enabled = false;

                //if gameobject has polyygoncollider remove it and add another polygoncollider to match the objectshape else just add one.
                if(gameObject.GetComponent<PolygonCollider2D>() != null)
                {
                    Destroy(gameObject.GetComponent<PolygonCollider2D>());
                    gameObject.AddComponent<PolygonCollider2D>();
                }
                else
                {
                    gameObject.AddComponent<PolygonCollider2D>();
                }

                break;
        }
    }


    private void HealthControl()
    {
        healthSize -= currentheathDecVal;
        //Debug.Log(healthSize);
        if (healthSize > 0)
        {
            healthControl.SetSize(healthSize);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "KillerBIrd")
        {
            hasCrashed = true;
            audioControl.playAudios("cutAxle");
            messageControls.ShowMessage("cutAxleResponse", true, "okay_wheel");
            if (gameObject.GetComponent<WheelJoint2D>() != null)
            {
                gameObject.GetComponent<WheelJoint2D>().enabled = false;
            }
        }
    }


    private void HideFirstMessage()
    {
        if (!isMessageOn)
        {
            messageControls.ShowNoMessage();
            isMessageOn = true;
        }
    }
}
