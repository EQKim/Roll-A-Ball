using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public TextMeshProUGUI countText; // Reference to the TextMeshProUGUI component for displaying the count
    public GameObject winTextObject;

    // Assign this in the inspector to the parent that contains all pickup objects
    public Transform pickupParent;
    // Assign this in the inspector to the parent that contains all enemy objects
    public Transform enemyParent;

    private int count;
    private int totalPickups;
    private float movementX;
    private float movementY;
    public float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody on Player. Please add a Rigidbody component.");
            enabled = false;
            return;
        }

        // Determine total pickups from the parent, if assigned
        if (pickupParent != null)
        {
            totalPickups = pickupParent.childCount;
        }
        else
        {
            // Fallback: count all active pickups in the scene by tag
            totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
            Debug.LogWarning("pickupParent not assigned. Using tag-based count for total pickups.");
        }

        count = 0;
        winTextObject.SetActive(false); // Ensure the win text is hidden at the start
        SetCountText(); 

        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        // Move the player based on input
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        rb.AddForce(movement * speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false); // Deactivate the other GameObject when the player collides with it
            count++; // Increment the count variable
            SetCountText(); // Update the count text to reflect the new count
        }
    }

    void SetCountText()
    {
        countText.text = $"Count: {count} / {totalPickups}"; // Update the text to show the current count
        if (count >= totalPickups && totalPickups > 0)
        {
            // If an enemy parent is assigned, destroy the parent (which removes all children)
            if (enemyParent != null)
            {
                Destroy(enemyParent.gameObject);
            }
            else
            {
                // Fallback: destroy each enemy GameObject individually by tag
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    if (enemy != null)
                    {
                        Destroy(enemy);
                    }
                }
            }

            winTextObject.SetActive(true); // Show the win text when all pickups are collected
        }
    }
}
