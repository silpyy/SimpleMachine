using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDecider : MonoBehaviour
{
    public OnHover onhover;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        onhover = FindObjectOfType<OnHover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onhover.jumpclick)
        {
            animator.SetBool("jumpflag", true);
        }
       
    }
}
