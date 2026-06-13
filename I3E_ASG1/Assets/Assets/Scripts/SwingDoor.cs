using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SwingDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float rotateSpeed;
    //for playerCollider to access
    public bool OpenDoor = false;
    private float doorTimer = 0f;
    [SerializeField] private float doorOpenDuration = 5f;
    private Quaternion closedRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets a timer so that when the player does not enter within a certain time the door closes
        if (OpenDoor)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer >= doorOpenDuration)
            {
                OpenDoor = false;
                doorTimer = 0f;
            }
        }
        //if door is fully open stop rotating
        if (OpenDoor && !(transform.rotation.y < -0.78539816339 || transform.rotation.y > 0.78539816339))
        {
            //checks which direction the player is compared to the door and opens towards the player
            if(player.gameObject.transform.position.z > transform.position.z)
            {
                transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up, -rotateSpeed*Time.deltaTime);
            }
        }
        // If door is not closed, rotate back to original rotation
        if (!OpenDoor && transform.rotation != closedRotation)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                closedRotation,
                rotateSpeed * Time.deltaTime
            );
        }

    }
}
