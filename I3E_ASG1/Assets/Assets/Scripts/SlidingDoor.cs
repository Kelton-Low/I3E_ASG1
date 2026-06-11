using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject == player.gameObject)
        {
            var animatorComponent = GetComponent<Animator>();
            animatorComponent.SetBool("IsOpen", true);
        }

    }
    void OnTriggerExit(Collider other)
    {
        var animatorComponent = GetComponent<Animator>();
        animatorComponent.SetBool("IsOpen", false);

    }
}
