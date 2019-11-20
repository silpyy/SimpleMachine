using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BasicMovementsOnly : MonoBehaviour
{
    public GameObject Skatepannel;
    public GameObject player;
    private bool flagroll=false;
    private OnHover onhover;
    void Start()
    {
        this.gameObject.SetActive(false);
        onhover = FindObjectOfType<OnHover>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!flagroll && onhover.skatestate())
        {
         transform.position = new Vector2(transform.position.x + .07f, transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "skatecollider")
        {
            flagroll = true;
            Skatepannel.SetActive(true);
        }
    }
}