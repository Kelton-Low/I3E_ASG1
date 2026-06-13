using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Transform player;

    void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject == player.gameObject)
        {
            var animatorComponent = GetComponent<Animator>();
            animatorComponent.SetBool("IsOpen", true); //open the door
        }

    }
    void OnTriggerExit(Collider other)
    {
        var animatorComponent = GetComponent<Animator>();
        animatorComponent.SetBool("IsOpen", false); //close the door

    }
}
