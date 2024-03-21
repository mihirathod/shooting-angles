using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Shooting : NetworkBehaviour
{
    public Firing Parent; 
    [SerializeField] float Moving_speed;
    private Rigidbody2D bullet;
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bullet.velocity = transform.up * Moving_speed;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!IsServer)
            return;
        
        
            Parent.DestroyServerRpc();
    }
   

}
