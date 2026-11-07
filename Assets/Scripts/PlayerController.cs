
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;

    private float movementX;
    private float movementY;

    public float speed = 0;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Reference to GameManager
    public GameManager gameManager;

    private bool gameEnded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        if (gameEnded)
            return;

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        if (gameEnded)
            return;

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameEnded)
            return;

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12 && !gameEnded)
        {
            gameEnded = true;

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));

            gameManager.ShowRestartButton();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameEnded)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameEnded = true;

            winTextObject.SetActive(true);

            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

            gameManager.ShowRestartButton();

            Destroy(gameObject);
        }
    }
}
