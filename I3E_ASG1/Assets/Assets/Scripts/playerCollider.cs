using System;
using System.Collections;
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
    private LineRenderer lineRenderer;
    [SerializeField] float beamWidth = 0.1f;
    [SerializeField] Color beamColour;
    [SerializeField] Material beamMaterial;
    public Vector3 externalVelocity = Vector3.zero;
    void Start()
    {
        rend = Gun.GetComponent<Renderer>();
        rend.enabled = false;
        print(rend.enabled);


        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = beamWidth;
        lineRenderer.material = beamMaterial;
        lineRenderer.startColor = beamColour;
        lineRenderer.endColor = beamColour;
        lineRenderer.enabled = false;
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
            if (rend.enabled &&
            swingDoor != null && !Physics.Raycast(Camera.transform.position, Camera.transform.forward, 10, CrystalMask))
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
                
                print("Shoot");
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, Gun.transform.position);
                lineRenderer.SetPosition(1, hitCrystal.point);

                StopCoroutine("HideLine");
                StartCoroutine("HideLine");
                Destroy(hitCrystal.collider.gameObject);
            }
        }
    }
    IEnumerator HideLine()
    {
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);

        Gizmos.DrawSphere(Camera.gameObject.transform.position, MaxDistanceInteraction);
    }
}
