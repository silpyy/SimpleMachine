using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{

    float dirX;
    float moveSpeed = 2f;
    Rigidbody2D rb;
    AudioSource audioSrc;
    bool isMoving = false;
    public AudioClip otherClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis ("Horizontal") * moveSpeed;

        if(rb.velocity.x != 0)
        isMoving = true;
        else 
        isMoving = false;

        if(isMoving){
            if(!audioSrc.isPlaying)
            audioSrc.Play();
        }
        else
        audioSrc.Stop ();
    }

    void OnTriggerEnter2D(Collider2D other){
        
         if (other.tag == "Coin"){
             audioSrc.PlayOneShot (otherClip, 0.7F);
         }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (dirX, rb.velocity.y);
    }
}
