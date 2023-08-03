using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour {
	
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;

	[Header("Attributes")]
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private float moveSpeed = 3f;

	private float mx;

	private void Update() {
		if (!IsOwner) return;

		mx = Input.GetAxisRaw("Horizontal");

		if (Input.GetButtonDown("Jump")) {
			Jump();
		}
	}

	private void FixedUpdate() {
		if (!IsOwner) return;

		rb.velocity = new Vector2(mx * moveSpeed, rb.velocity.y);
	}

	private void Jump() {
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
	}
}
