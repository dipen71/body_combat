using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.0f;  // Adjust this to change the speed of movement.
    private bool hasCollided = false; // Flag to check if collision has occurred
    public List<GameObject> breakable;
    public GameObject hide;
    public Vector3 moveDirection = Vector3.forward; // Direction to move the object
    public float moveSpeed = 5f; // Speed to move at
    public float destroyDistance = 10f; // Distance at which the object gets destroyed

    private float distanceTraveled = 0f; // Tracks the distance the object has traveled


    //for fading
    public float fadeDuration = 2.0f; // Duration in seconds
    private float currentFadeTime = 0.0f;

    public List<Material> materialsList = new List<Material>(); // List of materials to fade

    private bool shouldFade = false; // Flag to start fading

    private Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>(); // Store original colors
    private void Start()
    {
        // Store original colors when the script starts
        foreach (Material material in materialsList)
        {
            originalColors[material] = material.color;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Only move if no collision has occurred
        if (!hasCollided)
        {
            // Move the object along the world Z-axis when the "W" key is pressed.
            //if (Input.GetKey(KeyCode.W))
            //{
            //    transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
            //}
            float movementThisFrame = moveSpeed * Time.deltaTime;

            // Update the distance traveled
            distanceTraveled += movementThisFrame;

            // Move the object in the specified direction
            transform.Translate(moveDirection * movementThisFrame);

            // Check if the object has traveled the required distance to be destroyed
            if (distanceTraveled >= destroyDistance)
            {
                Destroy(gameObject);
            }
        }

        //for fading
        if (shouldFade)
        {
            // Increment the fade time
            currentFadeTime += Time.deltaTime;

            // Calculate the alpha value based on the current time
            float alphaValue = Mathf.Lerp(255f, 0f, currentFadeTime / fadeDuration);
            Debug.Log("hii");
            // Update the alpha value for each material in the list
            foreach (Material material in materialsList)
            {
                
                Color currentColor = material.color;
                currentColor.a = alphaValue / 255f;
                material.color = currentColor;
            }

            // Reset the fade time after reaching the target duration
            if (currentFadeTime >= fadeDuration)
            {
                currentFadeTime = 0.0f;
                shouldFade = false; // Reset the flag

                // Restore original colors
                foreach (var entry in originalColors)
                {
                    entry.Key.color = entry.Value;
                }

                // Disable the GameObject
                gameObject.SetActive(false);
                
            }
        }
    }

    
    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnCollisionEnter(Collision collision)
    {
        for(int i=0; i < breakable.Count;i++ )
        {
            breakable[i].gameObject.active = enabled;
        }
        hide.SetActive(false);
        StartFade();
        Debug.Log("tests");
        // Enable gravity on collision
      //  GetComponent<Rigidbody>().useGravity = true;

        // Set the flag to true to prevent further movement
        hasCollided = true;
    }

    private void StartFade()
    {
        shouldFade = true; // Set the flag to start fading
    }
}
