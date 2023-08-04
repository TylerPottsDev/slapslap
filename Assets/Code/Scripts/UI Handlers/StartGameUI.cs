using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class StartGameUI : MonoBehaviour {
    
	[SerializeField] private GameObject startGameUI;

	NetworkManager network;

	private void Awake() {
		network = NetworkManager.Singleton;
	}

	private void OnEnable() {
		network.OnClientConnectedCallback += CheckPlayerCount;
		network.OnClientDisconnectCallback += CheckPlayerCount;
	}

	private void OnDisable() {
		network.OnClientConnectedCallback -= CheckPlayerCount;
		network.OnClientDisconnectCallback -= CheckPlayerCount;
	}

	private void CheckPlayerCount (ulong clientId) {
		if (network.IsHost) {
			Debug.Log(network.ConnectedClients.Count + " users connected");

			if (network.ConnectedClients.Count > 1) {
				startGameUI.SetActive(true);
			} else {
				startGameUI.SetActive(false);
			}
		}
	}

	public void StartGame () {
		if (network.IsHost) {
			if (network.ConnectedClients.Count > 1) {

				network.SceneManager.LoadScene("Level01", LoadSceneMode.Single);

			}
		}
	}

}
