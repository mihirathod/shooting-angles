using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : MonoBehaviour
{
    public Button Host;
    public Button Client;


    private void Awake()
    {
        /*Host.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        Client.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });*/

        
    }

}
