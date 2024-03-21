using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Firing : NetworkBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] private Transform Firing_pt;
    [SerializeField] private List<GameObject> SpawnedBullets = new List<GameObject>();
    private AudioSource fireSound;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        fireSound =GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!IsOwner)
            return;
        //firin
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FiringServerRpc();
            
        }
    }

    [ServerRpc]

    private void FiringServerRpc(ServerRpcParams serverRpcParams = default)
    {
        GameObject Bulllet = Instantiate(BulletPrefab, Firing_pt.position, transform.rotation);
        SpawnedBullets.Add(Bulllet);
        Bulllet.GetComponent<Shooting>().Parent = this;
        Bulllet.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
        fireSound.Play();
    }

    [ServerRpc(RequireOwnership =false)]

    public void DestroyServerRpc()
    {
        //bulletdestroy == Bulllet
        GameObject bulletdestroy = SpawnedBullets[0];
        bulletdestroy.GetComponent<NetworkObject>().Despawn();
        SpawnedBullets.Remove(bulletdestroy);
        Destroy(bulletdestroy);
    }
}
