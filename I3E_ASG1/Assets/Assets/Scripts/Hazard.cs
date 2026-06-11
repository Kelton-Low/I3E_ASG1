using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float howLongBetweenDamage = 0.1f;
    [SerializeField] GameObject player;
    private float timeBetweenDamage = 0;
    private playerCollider playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<playerCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionStay(Collision collision)
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
                playerScript.playerHealth -= damage;
                print("Score: " + playerScript.playerHealth);
            }
        }

    }
}
