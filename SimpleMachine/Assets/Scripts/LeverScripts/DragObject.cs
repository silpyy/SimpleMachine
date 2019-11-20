using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    public Bounds colliderBound;
    public BoxCollider2D colliderObj;


    private Vector3 offset;
    private float pos_x;
    private float pos_y;
    private float limit_x_upper;
    private float limit_x_lower;
    private Vector2 lastMousePos;
    public float dragSpeed = 1f;
    public Vector2 objPos;
    private float deltaMovement;

    private bool isboundsDefined = false;
    private Vector2 mousePos;
    void start()
    {
        pos_y = transform.position.y;
        pos_x = transform.position.x;
        colliderObj = GetComponent<BoxCollider2D>();
        Debug.Log("Drag script called");
    }
    void Update()
    {
        if (!isboundsDefined)
        {
            limit_x_lower = colliderObj.bounds.min.x + 2;
            limit_x_upper = colliderObj.bounds.max.x - 2;
            isboundsDefined = true;
        }
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        
    }

    void OnMouseDown()
    {

        offset = gameObject.transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, pos_y, 10.0f));
    }

    void OnMouseDrag()
    {
        /*Debug.Log("fulcrum position : "+ transform.position.x);
        Debug.Log("mouse position : "+ mousePos.x);*/
        if (transform.position.x > limit_x_lower && transform.position.x < limit_x_upper)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, pos_y, 10.0f);
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
        else if (transform.position.x <= limit_x_lower && mousePos.x > limit_x_lower)
        {
            if(mousePos.x > limit_x_lower && mousePos.x < limit_x_upper)
            {
                Vector3 newPosition = new Vector3(Input.mousePosition.x, pos_y, 10.0f);
                transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;

            }
        }
        else if (transform.position.x >= limit_x_upper && mousePos.x < limit_x_upper)
        {
            if (mousePos.x > limit_x_lower && mousePos.x < limit_x_upper)
            {
                Vector3 newPosition = new Vector3(Input.mousePosition.x, pos_y, 10.0f);
                transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;

            }
        }
        /*else
        {
            transform.position = transform.position;
        }*/


    }
}
