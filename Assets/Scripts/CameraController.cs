
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned to CameraController!");
            return;
        }

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Check whether the player still exists
        if (player == null)
            return;

        // Follow the player
        transform.position = player.transform.position + offset;
    }
}
