using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using TMPro;
using System;

public class UIManager : MonoBehaviour {
	
	[Header("Join Code UI")]
    [SerializeField] private GameObject joinCodeBox;
	[SerializeField] private TextMeshProUGUI joinCodeText;

	[Header("Game UI")]
	[SerializeField] private GameObject startGameBTN;

	private void Start() {
		NetworkRelay.Instance.onJoinOrCreate.AddListener(OnGameJoin);
		NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
		NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;
	}

	private void OnDisconnected(ulong clientId) {
		CheckPlayerCount();
	}

	private void OnConnected(ulong clientId) {
		CheckPlayerCount();
	}

	private void OnGameJoin () {
		SetJoinCode();
	}

	private void SetJoinCode () {
		joinCodeBox.SetActive(true);
		joinCodeText.text = NetworkRelay.Instance.joinCode;
	}

	private void CheckPlayerCount () {
		Debug.Log($"CheckPlayerCount: ${NetworkManager.Singleton.IsHost}");
		
		if (NetworkManager.Singleton.IsHost) {
			Debug.Log(NetworkManager.Singleton.ConnectedClients.Count + " users connected");

			if (NetworkManager.Singleton.ConnectedClients.Count > 1) {
				startGameBTN.SetActive(true);
			} else {
				startGameBTN.SetActive(false);
			}
		}
	}
}
