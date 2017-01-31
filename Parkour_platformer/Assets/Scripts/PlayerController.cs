using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float m_runningSpeed;
	public float m_jumpingForce;
	public LayerMask groundLayer;
	private float maxJumpingForce = 1000;
	private Rigidbody2D m_rigidBody;
	private Animator m_animator;
	private bool isMoving;
	private bool isFalling;
	private bool isInAir;
	private float distanceToGround;

	// Use this for initialization
	void Start () {
		m_rigidBody = this.GetComponent<Rigidbody2D>();
		m_animator = this.GetComponent<Animator>();
		isMoving = true;
		isInAir = false;
		m_animator.SetBool("isMoving", isMoving);
		distanceToGround = this.GetComponent<BoxCollider2D>().bounds.max.y - m_rigidBody.transform.position.y;
		Debug.Log("distanceToGround: " + distanceToGround);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		handleMove();
		checkPlayerJumpingAnimation();
	}

	void handleMove () {
		m_rigidBody.velocity = new Vector2(m_runningSpeed, m_rigidBody.velocity.y);
		m_animator.SetFloat("input_x", m_runningSpeed > 0 ? 1 : -1);
		handleJump();
	}

	public void handleJump () {
		if (isGrounded() && !isInAir && Input.GetKeyDown(KeyCode.UpArrow)) {
			float jumpForce = m_jumpingForce * Time.deltaTime;
			m_rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			isInAir = true;
		}
	}

	public bool isGrounded() {
		RaycastHit2D hit = Physics2D.Raycast(m_rigidBody.transform.position, Vector2.down, distanceToGround + 0.5f, groundLayer);
		return hit.collider != null;
	}

	void checkPlayerJumpingAnimation() {
		bool isOnGround = isGrounded();
		if (!isOnGround && m_rigidBody.velocity.y < -0.1f) {
			isFalling = true;
			isInAir = true;
			m_animator.SetBool("isFalling", isFalling);
		}

		if (isFalling && isOnGround) {
			m_animator.SetTrigger("touchGround");
			m_rigidBody.velocity.Set(m_rigidBody.velocity.x, 0);
			isFalling = false;
			isInAir = false;
			m_animator.SetBool("isFalling", isFalling);
		}
	}

	public void handleCastAbility () {

	}
}
