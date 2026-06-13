using System;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    /// <summary>
    /// Used to close the door behind me
    /// </summary>
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform doorMesh;
    [SerializeField] private AudioSource doorSound;

    [Header("Settings")]
    [SerializeField] private float speed = 2f;

    /// <summary>The rotation of the door when fully closed.</summary>
    private Quaternion closedRotation;

    /// <summary>The rotation of the door when fully open.</summary>
    private Quaternion openRotation;

    /// <summary>Whether the door is currently open or transitioning to open.</summary>
    private bool isOpen = true;

    void Start()
    {
        //setting my open and closed states
        closedRotation = doorMesh.rotation * Quaternion.Euler(0, -90f, 0);
        openRotation = doorMesh.rotation;
    }

    void Update()
    {
        //Animates my rotation of my door
        doorMesh.rotation = Quaternion.Lerp(
            doorMesh.rotation,
            isOpen ? openRotation : closedRotation,
            speed * Time.deltaTime
        );
    }
    
    //Detects the player entering or leaving the vicinity of the door
    void OnTriggerEnter(Collider other)
    {
        doorSound.Play();
        if(other.gameObject == player)
        {
            isOpen = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        doorSound.Play();

        if(other.gameObject == player)
        {
            isOpen = false;
        }
    }
}
