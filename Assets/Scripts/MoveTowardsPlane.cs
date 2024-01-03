using UnityEngine;

public class MoveTowardsPlane : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.forward; // Direction to move the object
    public float moveSpeed = 5f; // Speed to move at
    public float destroyDistance = 10f; // Distance at which the object gets destroyed

    private float distanceTraveled = 0f; // Tracks the distance the object has traveled

    private void Update()
    {
        // Calculate the distance moved during this frame
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
}

   
