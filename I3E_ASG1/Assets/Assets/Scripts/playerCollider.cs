using System;
using UnityEngine;

public class playerCollider : MonoBehaviour
{
    [SerializeField] private LayerMask CollectibleMask;
    [SerializeField] private LayerMask DoorMask;
    [SerializeField] private LayerMask CrystalMask;
    [SerializeField] private GameObject Camera;
    [SerializeField] private float MaxDistanceInteraction;
    [SerializeField] private GameObject Gun;
    private int score = 0;
    public int playerHealth;
    private Renderer rend;
    void Start()
    {
        rend = Gun.GetComponent<Renderer>();
        rend.enabled = false;
        print(rend.enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteract()
    {
        //Check for collectible
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, MaxDistanceInteraction, CollectibleMask))
        {
            CoinValue coinValue = hit.collider.GetComponent<CoinValue>();

            Destroy(hit.collider.gameObject);

    
            score += coinValue.Value;

            print("Score: "+ score);
            if (coinValue.IsGun == true)
            {
                rend.enabled = true;
            }

        }
        //Check for door
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hitDoor, 10, DoorMask))
        {
            print("Found Door");
            SwingDoor swingDoor = hitDoor.collider.GetComponentInParent<SwingDoor>();
            if (rend.enabled && swingDoor != null)
            {
                swingDoor.OpenDoor = true;
            }
        }
    }
    void OnShoot()
    {
        if (rend.enabled)
        {
            if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hitCrystal, 10, CrystalMask))
            {
                Destroy(hitCrystal.collider.gameObject);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);

        Gizmos.DrawSphere(Camera.gameObject.transform.position, MaxDistanceInteraction);


    }
}
