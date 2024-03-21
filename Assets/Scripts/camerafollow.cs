using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public float smoothSpeed = 5f;  // Adjust the camera follow speed as needed.
    private GameObject localPlayer;  // Reference to the local player GameObject.

    private void LateUpdate()
    {
        // Check if the local player reference is set.
        if (localPlayer != null)
        {
            // Calculate the desired camera position.
            Vector3 desiredPosition = localPlayer.transform.position;
            desiredPosition.z = transform.position.z;  // Keep the camera's Z position constant.

            // Smoothly move the camera towards the desired position.
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }

    // Method to set the local player GameObject to follow.
    public void SetLocalPlayer(GameObject newLocalPlayer)
    {
        localPlayer = newLocalPlayer;
    }
}
