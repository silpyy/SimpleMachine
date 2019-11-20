using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    private bool jumpflag = true;
    private Rigidbody2D m_Rigidbody2D;
    public float energyvalue;
    public Slider EnergyBar;
    public float energy = 100;
    private float currentEnergy;
    public GameObject EnergyFinish;
    public respawnpoint1 respawnPoint;
    public Respawnbuttonposition respawnControl;
    private static bool controllers = false;


    public void Start()
    {
        energyvalue = 0;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        currentEnergy = energy;
        respawnPoint = FindObjectOfType<respawnpoint1>();
        Debug.Log("Should respawn: Start");
        respawnControl = FindObjectOfType<Respawnbuttonposition>();
        Debug.Log("Should respawn: " + respawnPoint.RespawnState());
        if (respawnPoint.RespawnState())
        {
            respawnControl.respawnPlayer();
        }
        // GameObject.Find("EnergyFinish").SetActive(false);
    }
    // private void Awake()
    //     {
    //     }

    // Update is called once per frame
    void Update()
    {
        if (!controllers)
        {
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal") * 3, 0.0f, 0.0f);
            transform.position = transform.position + horizontal * Time.deltaTime;
        }
        
        // if (Input.GetKeyDown("space") && jumpflag)
        // {
        //     jumpflag = false;
        //     Vector3 vertical = new Vector3(Input.GetAxis("Vertical")*3,0.0f, 0.0f);
        //     m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        // }

        if (Input.GetButton("Horizontal"))
        {
            //  energyvalue += 1;
            //  currentEnergy-=(energyvalue);
            EnergyBar.value = ++energyvalue;
            if (EnergyBar.value > 5999)
            {
                EnergyFinish.SetActive(true);
                Time.timeScale = 0f;
                // isKeysEnabled = false;
            }
            //  if(EnergyBar.value > 2500){
            //     // isKeysEnabled = false;
            //     EnergyBar.color= red
            //  }
            //  Debug.Log(energyvalue);
        }
        if (Input.GetKeyDown("space") && jumpflag)
        {
               transform.Translate(Vector3.up * 195   * Time.deltaTime, Space.World);
                jumpflag = false;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "platform")
        {
            jumpflag = true;
        }
        if( col.gameObject.tag == "slider")
        {
            animator.SetBool("slider", true);
            controllers = true;
        }
        if (col.gameObject.tag == "sliderend")
        {
            animator.SetBool("slider", false);
            controllers = false;
        }
        if (col.gameObject.tag == "granny")
        {
            animator.SetBool("grandcollides", true);
            controllers = true;
        }
        if (col.gameObject.tag == "HouseCollider")
        {
            animator.SetBool("grandcollides", false);
            controllers = false;
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }


}