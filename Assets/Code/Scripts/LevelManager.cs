using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class LevelManager : MonoBehaviour {
    
	private NetworkManager network = null;

	private void Start() {
		network = NetworkManager.Singleton;
		
		network.OnServerStarted += LoadLobby;
	}

	private void OnEnable() {
		if (network != null) {
			network.OnServerStarted += LoadLobby;
		}
	}

	private void OnDisable() {
		network.OnServerStarted -= LoadLobby;
	}

	private void LoadLobby () {
		SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
	}

	private void DisconnectedFromServer (ulong clientId) {
		Debug.Log("Someone was disconnected");
		if (clientId == network.LocalClientId) {
			Debug.Log("You were disconnected");
		}
	}



}
