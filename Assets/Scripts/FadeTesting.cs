using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTesting : MonoBehaviour
{
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

    private void Update()
    {
        if (shouldFade)
        {
            // Increment the fade time
            currentFadeTime += Time.deltaTime;

            // Calculate the alpha value based on the current time
            float alphaValue = Mathf.Lerp(255f, 0f, currentFadeTime / fadeDuration);

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

}
