using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

public class NetworkRelay : MonoBehaviour {

	public static NetworkRelay Instance;

	public string joinCode;

	public UnityEvent onJoinOrCreate = new UnityEvent();

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.LogError("Error: Two Network Relays are present");
		}
	}
    
	private async void Start() {
		await UnityServices.InitializeAsync();

		AuthenticationService.Instance.SignedIn += () => {
			Debug.Log("User signed in Anonymously: " + AuthenticationService.Instance.PlayerId);
		};

		await AuthenticationService.Instance.SignInAnonymouslyAsync();
	}

	public async void CreateRelay() {
		try {
			Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

			joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
			Debug.Log("Creating room: " + joinCode);

			RelayServerData data = new RelayServerData(allocation, "dtls");

			NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

			NetworkManager.Singleton.StartHost();

			onJoinOrCreate.Invoke();
		} catch (RelayServiceException e) {
			Debug.Log(e);
		}
	}

	public async void JoinRelay(string _joinCode) {
		try {
			Debug.Log("Joining room: " + _joinCode);
			JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(_joinCode);

			RelayServerData data = new RelayServerData(allocation, "dtls");

			NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

			NetworkManager.Singleton.StartClient();
			
			joinCode = _joinCode;

			onJoinOrCreate.Invoke();
		} catch (RelayServiceException e) {
			Debug.Log(e);
		}
	}

}
