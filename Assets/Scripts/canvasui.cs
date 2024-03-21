using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class canvasui : NetworkBehaviour
{
    public Text livesText;

    private Healthmanager healthManager; // Reference to Healthmanager script

    private bool healthManagerInitialized = false; // Flag to check if healthManager has been initialized

    private void Update()
    {
        // Check if the Healthmanager script has been initialized
        if (!healthManagerInitialized)
        {
            // Try to find the Healthmanager script using GetComponent
            healthManager = GetComponent<Healthmanager>();

            if (healthManager != null)
            {
                // Healthmanager script found, initialize and start updating UI
                healthManagerInitialized = true;
                UpdateLivesText(healthManager.currentLives.Value);
            }
        }
        else if (IsLocalPlayer && healthManager != null)
        {
            // Update the UI as usual once the Healthmanager script is initialized
            // Check if the current lives have changed since the last frame
            if (healthManager.currentLives.Value != previousLives)
            {
                // Update the UI text with the new number of lives
                UpdateLivesText(healthManager.currentLives.Value);
                // Store the current lives for comparison in the next frame
                previousLives = healthManager.currentLives.Value;
            }
        }
    }

    private int previousLives = -1; // Store the previous number of lives

    private void UpdateLivesText(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives.ToString();
        }
    }
}
