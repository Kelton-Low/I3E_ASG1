using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SwingDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float rotateSpeed;
    public bool OpenDoor = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenDoor && !(transform.rotation.y < -0.78539816339 || transform.rotation.y > 0.78539816339))
        {
            print(transform.rotation.y);

            if(player.gameObject.transform.position.y > transform.position.y)
            {
                transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up, -rotateSpeed*Time.deltaTime);
            }
        }
    }
}
