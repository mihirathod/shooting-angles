using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;

public class testLobby : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("signedIn" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }


    private  async System.Threading.Tasks.Task<string> CreateRelay()
    {
        //add asia
        try
        {
            Allocation allocation= await RelayService.Instance.CreateAllocationAsync(4);
            string joincode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joincode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

            return joincode;

        }
        catch(RelayServiceException e)
        {
            Debug.Log(e);
            return null;
        }
    }

    private async void joinRelayy(string joincodee)
    {
        try
        {
            await RelayService.Instance.JoinAllocationAsync(joincodee);

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

    }
}
