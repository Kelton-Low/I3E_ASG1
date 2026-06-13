using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
public class GrappleBehavior : MonoBehaviour
{
    /// <summary>
    /// Set parameters for how the grapple feels to the player
    /// </summary>
    [Header("Grapple Settings")]
    [SerializeField] private float grappleRange = 20f;
    [SerializeField] private float grappleSpeed = 15f;
    [SerializeField] private float arrivalDistance = 1.5f;
    [SerializeField] private LayerMask grappleMask;
    [SerializeField] private float grappleLaunchForce = 10f;

    /// <summary>
    /// Outside objects
    /// </summary>
    [Header("References")]
    [SerializeField] private GameObject playerCamera;
    private CharacterController controller;
    private FirstPersonController fpsController;
    private bool isGrappling = false;
    private Vector3 grappleTarget;
    private LineRenderer lineRenderer;

    /// <summary>
    /// Set parameters for the glowing rope
    /// </summary>
    [Header("Grapple Rope")]
    [SerializeField] float beamWidth = 0.1f;
    [SerializeField] Color beamColour;
    [SerializeField] Material beamMaterial;
    [SerializeField] GameObject Gun;
    [SerializeField] private AudioSource shootSound;

    /// <summary>
    /// Checks if the grapple point is above the player so an up force can be applied
    /// </summary>
    private bool isAbove;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        fpsController = GetComponent<FirstPersonController>();

        //add the line renderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = beamWidth;
        lineRenderer.material = beamMaterial;
        lineRenderer.startColor = beamColour;
        lineRenderer.endColor = beamColour;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isGrappling)
            MoveTowardTarget();
    }

    // Called when grapple button is pressed
    // Fires a raycast from the camera, saves the hit point
    void OnShoot(InputValue value)
    {
        // If already grappling, cancel it
        if (isGrappling)
        {
            StopGrapple();
            return;
        }
        
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, grappleRange, grappleMask))
        {
            shootSound.Play();
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, Gun.transform.position);
            lineRenderer.SetPosition(1, hit.point);
            grappleTarget = hit.point;
            if(grappleTarget.y > transform.position.y)
            {
                isAbove = true;
            }
            else
            {
                isAbove = false;
            }
            isGrappling = true;
            fpsController.enabled = false; // Disable normal movement while grappling
            Debug.Log("Grappling to: " + grappleTarget);
        }
    }

    // Moves the player toward the grapple target each frame
    // Stops when close enough
    void MoveTowardTarget()
    {
        Vector3 direction = (grappleTarget - transform.position).normalized;
        controller.Move(direction * grappleSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, grappleTarget) < arrivalDistance)
            StopGrapple();
    }

    // Stops grappling and re-enables normal movement
    void StopGrapple()
    {
        isGrappling = false;
        fpsController.enabled = true;
        lineRenderer.enabled = false;
        if (isAbove)
        {
            fpsController._verticalVelocity = grappleLaunchForce;

        }
    }
}
