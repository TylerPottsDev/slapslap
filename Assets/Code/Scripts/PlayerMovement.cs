using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour {
	
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform feet;

	[Header("Attributes")]
	[SerializeField] private int maxJumps = 2;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private float moveSpeed = 3f;
	[SerializeField] private LayerMask platformLayers;

	private float mx;

	private bool isGrounded = false;
	private int jumpCount = 0;

	private void Update() {
		if (!IsOwner) return;

		isGrounded = Physics2D.OverlapCircle(feet.position, 0.25f, platformLayers);

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
		if (isGrounded) jumpCount = 0;

		if (isGrounded || jumpCount < maxJumps) {
			jumpCount++;
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
	}
}
