using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private Vector3 playerPosition;
    public float cameraSmoothing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        if( player.transform.position.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, cameraSmoothing * Time.deltaTime);
    }
}

/* to make the camera follow a player
 1. Define a player
 2. Define the offset for camera, so as to keep the view area of the camera larger
 3. find the player position
 4. Check the direction player is facing, so as to maintain the camera distance from the player
 5. Player's changing direction's based on the x-value of the player, so can use that for camera position by additio or subtraction of offset
 
 6. Use lerp to smoothen the transition of camera between left and right, and vice-versa

 */
