using UnityEngine;
using UnityEngine.SceneManagement;


public class TeleportToScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "Scene2";
    [SerializeField] private GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            print("move to scene 2");
            SceneManager.LoadScene(sceneName);
        }
    }
}
