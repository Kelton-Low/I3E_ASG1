using System;
using System.Collections;
using UnityEngine;

public class playerCollider : MonoBehaviour
{
    /// <summary>
    /// Handles player interactions including collecting items, opening doors, and shooting crystals.
    /// </summary>

    [Header("Masks")]
    [SerializeField] private LayerMask CollectibleMask;
    [SerializeField] private LayerMask DoorMask;
    [SerializeField] private LayerMask CrystalMask;

    [Header("Interaction")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private float MaxDistanceInteraction;

    [Header("Gun")]
    [SerializeField] private GameObject Gun;

    [Header("UI")]
    [SerializeField] private UIManager myUIManager;
    [SerializeField] private int maxScore;

    [Header("Audio")]
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource doorSound;

    [Header("Stats")]
    public int playerHealth;
    private float maxHealth;
    private int score = 0;
    
    /// <summary>
    /// Used for triple t script to move the player away from triple t
    /// </summary>
    [Header("Physics")]
    public Vector3 externalVelocity = Vector3.zero;

    /// <summary>Renderer for the gun object, used to toggle gun visibility.</summary>
    private Renderer rend;



    void Start()
    {
        //Get component of gun and make the score 0
        rend = Gun.GetComponent<Renderer>();
        rend.enabled = false;
        myUIManager.UpdateScore(score, maxScore);
        maxHealth = playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //Die mechanic
        if(playerHealth <= 0)
        {
            myUIManager.ShowDiePanel(score);
            myUIManager.UpdateHealthUI(0, 10);
        }
        else
        {
            //Moves my health bar
            myUIManager.UpdateHealthUI(playerHealth, maxHealth);
        }
        //Updates my hint texts
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, MaxDistanceInteraction, CollectibleMask))
        {
            myUIManager.ShowHintingText($"Press \"e\" to collect");
        }
        else
        {
            myUIManager.ShowHintingText("");
        }
    }

    void OnInteract()
    {
        //Check for collectible
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, MaxDistanceInteraction, CollectibleMask))
        {
            CoinValue coinValue = hit.collider.GetComponent<CoinValue>();

            Destroy(hit.collider.gameObject);
            collectSound.Play();


            score += coinValue.Value;

            print("Score: "+ score);
            myUIManager.UpdateScore(score, maxScore);
            //checks for gun status
            if (coinValue.IsGun == true)
            {
                rend.enabled = true;
            }

        }
        //Check for door
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hitDoor, 10, DoorMask))
        {
            doorSound.Play();
            print("Found Door");
            SwingDoor swingDoor = hitDoor.collider.GetComponentInParent<SwingDoor>();
            //open the door if score is good
            if (rend.enabled &&
            swingDoor != null && 
            !Physics.Raycast(Camera.transform.position, Camera.transform.forward, 10, CrystalMask) &&
            score >= maxScore)
            {
                swingDoor.OpenDoor = true;
            }
        }
    }
    void OnShoot()
    {
        //Destroys the crystal outside my doors
        if (rend.enabled)
        {
            if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hitCrystal, 10, CrystalMask))
            {
                
                print("Shoot");

                Destroy(hitCrystal.collider.gameObject);
            }
        }
    }
    void OnMenu()
    {
        myUIManager.ToggleMenuPanel();
    }

    void OnDrawGizmos()
    {
        //Used to see my raycast range
        Gizmos.color = new Color(1, 0, 0, 0.1f);

        Gizmos.DrawSphere(Camera.gameObject.transform.position, MaxDistanceInteraction);
    }
}
