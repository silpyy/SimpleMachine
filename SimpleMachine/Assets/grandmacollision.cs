using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandmacollision : MonoBehaviour
{
    public GameObject choice1;
    public int count = 1;
    public GameObject grandmacollides;
    public Animator animator;
    private float positionvariable;
    private void Start()
    {
        positionvariable = transform.position.x;
    }

    private void Update()
    {
        
        if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("flowwheel", true);

        }
        else
        {
            animator.SetBool("flowwheel", false);

        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && count == 1)
        {
            choice1.SetActive(true);
            Time.timeScale = 0f;
            count++;
        }
        if (col.gameObject.tag == "GameController")
        {
            grandmacollides.SetActive(true);
            Time.timeScale = 0f;
        }
      
        


    }
}
