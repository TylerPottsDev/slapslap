using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
	[SerializeField] private GameObject joinCodeBox;
	[SerializeField] private TextMeshProUGUI joinCodeText;

	private NetworkRelay relay;

	private void Start() {
		relay = NetworkRelay.Instance;
		relay.onJoinOrCreate.AddListener(SetJoinCode);
	}

	private void SetJoinCode () {
		joinCodeBox.SetActive(true);
		joinCodeText.text = relay.joinCode;
	}
}
