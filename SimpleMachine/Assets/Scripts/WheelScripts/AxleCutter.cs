using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxleCutter : MonoBehaviour
{
    private Vector2 curPosition;
    private Vector2 initialPosition;
    private bool moveUp = false;
    private void Start()
    {
        initialPosition = gameObject.transform.position;
        Invoke("MoveUp", 1f);
    }
    private void Update()
    {
        curPosition = gameObject.transform.position;
        
    }

    private void MoveUp()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        Invoke("MoveDown", 3f);
    }
    private void MoveDown()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        Invoke("MoveUp", 5f);
    }


    

}
