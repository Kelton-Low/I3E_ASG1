using UnityEngine;

public class TripleT : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float wanderSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float wanderRadius = 5f;
    [SerializeField] private float chaseDuration = 3f;

    [Header("Vision")]
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float eyeHeight = 0.6f;
    [SerializeField] private LayerMask visionBlockMask;

    [Header("Push")]
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private int damage = 1;

    [Header("Wall Avoidance")]
    [SerializeField] private float avoidanceDuration = 0.8f;
    [SerializeField] private float avoidDistance = 1.5f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private AudioSource damageSound;

    /// <summary>The current destination the enemy is wandering toward.</summary>
    private Vector3 wanderTarget;

    /// <summary>Counts down how long the enemy chases before returning to wandering.</summary>
    private float chaseTimer = 0f;

    /// <summary>Whether the enemy is currently chasing the player.</summary>
    private bool isChasing = false;

    /// <summary>The enemy's Rigidbody component used for movement.</summary>
    private Rigidbody rb;

    /// <summary>Reference to the player's script to apply damage to health.</summary>
    private playerCollider playerScript;

    /// <summary>Whether the enemy is currently executing a wall avoidance maneuver.</summary>
    private bool isAvoidingWall = false;

    /// <summary>The direction the enemy moves during wall avoidance.</summary>
    private Vector3 avoidanceDirection;

    /// <summary>Counts down how long the enemy continues the avoidance maneuver.</summary>
    private float avoidanceTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<playerCollider>();
        rb = GetComponent<Rigidbody>();
        PickNewWanderTarget();
    }

    // Update is called once per frame
    void Update()
    {
        //start chasing if it sees the player
        if (CanSeePlayer())
        {
            isChasing = true;
            chaseTimer = chaseDuration;
        }
        //set a timer for how long the chase happens
        if (isChasing)
        {
            chaseTimer -= Time.deltaTime;
            ChasePlayer();
            if(chaseTimer <= 0f)
            {
                isChasing = false;
            }
        }
        else
        {
            Wander();
        }


    }
    //Checks if player is in eyeline of triple t
    bool CanSeePlayer()
    {
        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;
        Vector3 directionToPlayer = (player.position - eyePosition).normalized;
        float distanceToPlayer = Vector3.Distance(eyePosition, player.position);

        if (distanceToPlayer > visionRange)
            return false;

        if (Physics.Raycast(eyePosition, directionToPlayer, out RaycastHit hit, visionRange, ~visionBlockMask))
        {
            return hit.transform == player;
        }

        return false;
    }
    void ChasePlayer()
    {
        //Find direction of the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction = GetWallAvoidanceDirection(direction);


        Vector3 newPosition = transform.position + direction * chaseSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Face the player
        transform.LookAt(new Vector3(
            player.position.x, 
            transform.position.y, 
            player.position.z
        ));
    }
    //Gives triple t an idle state
    void Wander()
    {
        Vector3 direction = (wanderTarget - transform.position).normalized;
        direction = GetWallAvoidanceDirection(direction);

        Vector3 newPosition = transform.position + direction * wanderSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Face the place
        transform.LookAt(new Vector3(
            transform.position.x + direction.x,
            transform.position.y,
            transform.position.z + direction.z
        ));

        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
            PickNewWanderTarget();
    }
     Vector3 GetWallAvoidanceDirection(Vector3 intendedDirection)
    {
        // If already avoiding, keep going in same direction until timer runs out
        if (isAvoidingWall)
        {
            avoidanceTimer -= Time.deltaTime;

            // Check if the avoidance path itself is now blocked
            if (Physics.Raycast(transform.position, avoidanceDirection, avoidDistance, visionBlockMask))
            {
                // Avoidance path is also blocked, pick the other perpendicular
                avoidanceDirection = -avoidanceDirection;
            }

            if (avoidanceTimer <= 0f)
                isAvoidingWall = false;

            return avoidanceDirection;
        }

        // Check if wall is ahead
        if (Physics.Raycast(transform.position, intendedDirection, avoidDistance, visionBlockMask))
        {
            // Get the two perpendicular directions (left and right of intended direction)
            Vector3 perpRight = new Vector3(intendedDirection.z, 0, -intendedDirection.x);
            Vector3 perpLeft = new Vector3(-intendedDirection.z, 0, intendedDirection.x);

            // Pick whichever side is clear, prefer right
            if (!Physics.Raycast(transform.position, perpRight, avoidDistance, visionBlockMask))
            {
                avoidanceDirection = perpRight;
            }
            else if (!Physics.Raycast(transform.position, perpLeft, avoidDistance, visionBlockMask))
            {
                avoidanceDirection = perpLeft;
            }
            else
            {
                // Both sides blocked, turn around
                avoidanceDirection = -intendedDirection;
            }

            isAvoidingWall = true;
            avoidanceTimer = avoidanceDuration;
            return avoidanceDirection;
        }

        return intendedDirection;
    }

    //Finds a new target to move to inside a circle
    void PickNewWanderTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector3(
            transform.position.x + randomCircle.x,
            transform.position.y,
            transform.position.z + randomCircle.y
        );
    }

    //Damages player and pushes them away so they have time to escape
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            CharacterController playerCC = collision.gameObject.GetComponent<CharacterController>();
            if (playerCC != null)
            {
                damageSound.Play();
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                playerCC.Move(pushDirection * pushForce * Time.deltaTime);
                playerScript.playerHealth -= damage;
                print("Player Health: " + playerScript.playerHealth);
            }
        }
    }
    void OnDrawGizmos()
    {
        // Vision ray
        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;
        Gizmos.DrawLine(eyePosition, eyePosition + transform.forward * visionRange);

        Gizmos.DrawSphere(wanderTarget, 0.2f);

        // Vision range sphere
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(eyePosition, visionRange);
    }

}
