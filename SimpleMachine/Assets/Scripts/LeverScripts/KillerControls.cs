using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerControls : MonoBehaviour
{
    private Vector2 initialPosition;
    private bool moveLeft = false;
    private bool moveRight = true;

    [SerializeField] private float guardDistance;
    void Start()
    {
        initialPosition = transform.position;
        //Debug.Log(initialPosition);
    }

    void Update()
    {
        MoveKiller();
    }

    private void MoveKiller()
    {
        if (moveRight) {
            MoveRight();
        }

        if(moveLeft){
            MoveLeft();
        }

    }
    private void MoveRight()
    {
        transform.position = new Vector2(transform.position.x + .03f, initialPosition.y);
        transform.localScale = new Vector2(2.009687f, 2.230312f);
        if (transform.position.x >= initialPosition.x + guardDistance)
        {
            moveRight = false;
            moveLeft = true;
        }

    }
    private void MoveLeft()
    {
        transform.position = new Vector2(transform.position.x - .03f, initialPosition.y);
        transform.localScale = new Vector2(-2.009687f, 2.230312f);
        if (transform.position.x <= initialPosition.x)
        {
             moveLeft = false;
             moveRight = true;
        }

    }
}
