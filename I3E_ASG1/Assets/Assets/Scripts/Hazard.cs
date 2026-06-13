using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float howLongBetweenDamage = 0.1f;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioSource damageSound;

    /// <summary>Tracks time elapsed since last damage tick to enforce damage cooldown.</summary>
    private float timeBetweenDamage = 0;

    /// <summary>Reference to the player's collider script to apply damage to health.</summary>
    private playerCollider playerScript;

    void Start()
    {
        playerScript = player.GetComponent<playerCollider>();
    }


    //Damages player upon first contact with collider
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == player)
        {
            damageSound.Play();
            playerScript.playerHealth -= damage;
            print("Health: " + playerScript.playerHealth);

        }
    }
    //Damages player when staying inside the hazard
    void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject == player)
        {
            if (howLongBetweenDamage >= timeBetweenDamage)
            {
                timeBetweenDamage += Time.deltaTime;
            }
            else
            {
                timeBetweenDamage = 0;
                damageSound.Play();
                playerScript.playerHealth -= damage;
                print("Health: " + playerScript.playerHealth);
            }
        }

    }
}
