using UnityEngine;

public class CoinValue : MonoBehaviour
{
    /// <summary>
    /// Just used to let the playerCollider script know the value of the collectible
    /// </summary>
    public int Value = 1;
    public bool IsGun = false;
    [SerializeField] private float rotateSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime);
    }
}
