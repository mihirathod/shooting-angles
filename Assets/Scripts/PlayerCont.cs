using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCont : NetworkBehaviour
{
    public float moving_speed;
    public float rotation_speed;
    [SerializeField] Rigidbody2D player;
    public float hormove;
    public float vermove;
    [SerializeField] 
    Vector3[] spawnPositions = new Vector3[4];

    public override void OnNetworkSpawn()
    {

        UpdateposServerRpc();
        if (IsLocalPlayer)
        {
            // Find the CameraFollow script on the camera GameObject
            camerafollow cameraFollow = Camera.main.GetComponent<camerafollow>();

            // Set the local player GameObject for camera following
            if (cameraFollow != null)
            {
                cameraFollow.SetLocalPlayer(this.gameObject);
            }
        }

    }

    public void Update()

    {
        if (!IsOwner) return;
        hormove = Input.GetAxis("Horizontal");
        vermove = Input.GetAxis("Vertical");
        player.velocity = new Vector2(hormove * moving_speed, player.velocity.y);
        player.velocity = new Vector2(player.velocity.x, vermove * moving_speed);

        //player.AddForce(new Vector2(hormove* moving_speed, 0), ForceMode2D.Force);

        if (hormove != 0 || vermove != 0)
        {
            Quaternion Target_rotation = Quaternion.LookRotation(transform.forward, new Vector3(hormove, vermove, transform.rotation.z));
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, Target_rotation, rotation_speed * Time.fixedDeltaTime);
            player.MoveRotation(rotation);
        }
    }

    [ServerRpc(RequireOwnership =false)]

    private void UpdateposServerRpc()
    {


        int randomIndex = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPosition = spawnPositions[randomIndex];

        // Set the player's position and rotation
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.GetComponent<Shooting>() &&GetComponent<NetworkObject>().OwnerClientId !=collider.GetComponent<NetworkObject>().OwnerClientId)
        {

            DamageServerRpc();
        }

    }
    [ServerRpc(RequireOwnership =false)]

    private void DamageServerRpc()
    {

        GetComponent<Healthmanager>().TakeDamage(10);

    }

}







