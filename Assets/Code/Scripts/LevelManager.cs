using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class LevelManager : MonoBehaviour {

	private void Start() {
		NetworkManager.Singleton.OnServerStarted += LoadLobby;
		NetworkManager.Singleton.OnClientDisconnectCallback += DisconnectedFromServer;
	}

	private void Update() {
		if (NetworkManager.Singleton.ShutdownInProgress) {
			Debug.Log("Unexpectedly disconnected");
			ReturnToMenu();
		}
	}

	private void LoadLobby () {
		SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
	}

	private void DisconnectedFromServer (ulong clientId) {
		Debug.Log("Someone was disconnected");
		
		if (NetworkManager.Singleton.LocalClientId == clientId) {
			Debug.Log("You were disconnected");
			NetworkManager.Singleton.Shutdown();
			ReturnToMenu();
		}
	}

	private void ReturnToMenu() {
		Debug.Log("Returning to menu");

		for (int i = 0; i < SceneManager.sceneCount; i++) {
			Scene scene = SceneManager.GetSceneAt(i);

			if (scene.name != "Base" && scene.isLoaded) {
				SceneManager.UnloadSceneAsync(scene);
			}
		}
	}



}
