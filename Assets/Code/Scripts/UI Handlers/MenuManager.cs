using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour {
    
	[SerializeField] private TMP_InputField join_input;
	[SerializeField] private Button joinButton;

	private NetworkRelay relay;

	private void Start() {
		relay = NetworkRelay.Instance;
	}

	public void CreateGame () {
		relay.CreateRelay();
		gameObject.SetActive(false);
	}

	public void JoinGame () {
		relay.JoinRelay(join_input.text);
		gameObject.SetActive(false);
	}

	public void JoinInputChange () {
		if (join_input.text == "" || join_input.text == null) {
			joinButton.interactable = false;
		} else {
			joinButton.interactable = true;
		}
	}

}
