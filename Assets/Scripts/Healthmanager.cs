using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Healthmanager : NetworkBehaviour
{
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();
    public GameObject player;
    public Vector3[] respawnPositions = new Vector3[4];
    public NetworkVariable<int> maxLives = new NetworkVariable<int>(3);
    public NetworkVariable<int> currentLives = new NetworkVariable<int>();
    public Vector3 deathPos;
    public GameObject DiedScreen;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        currentHealth.Value = 100;
        currentLives = maxLives;
    }
    


    public void TakeDamage(int damageAmount)
    {
        if (IsServer)
        {
            currentHealth.Value -= damageAmount;

            if (currentHealth.Value <= 0)
            {
                currentLives.Value--;
                

                if (currentLives.Value <= 0)
                {
                    DestroyPlayer();
                }
                else
                {
                    RespawnPlayer();
                }
            }
        }
    }

    private void RespawnPlayer()
    {
        currentHealth.Value = 100;
        int randomIndex = Random.Range(0, respawnPositions.Length);
        Vector3 respawnPosition = respawnPositions[randomIndex];

        RespawnPlayerClientRpc(respawnPosition);
    }

    [ClientRpc]
    private void RespawnPlayerClientRpc(Vector3 respawnPosition)
    {
        if (IsLocalPlayer)
        {
            player.transform.position = respawnPosition;
        }
    }
    private void DestroyPlayer()
    {
        if (IsServer)
        {
            // Ensure the player GameObject exists
            if (player != null)
            {
                // Destroy the player GameObject
                NetworkObject.Destroy(player.GetComponent<NetworkObject>());
                player.transform.position = new Vector3(0, 404, 0);
                Instantiate(DiedScreen);

            }
        }
    }
}
