using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float m_runningSpeed;
	private Rigidbody2D m_rigidBody;
	private Animator m_animator;
	private bool isMoving;

	// Use this for initialization
	void Start () {
		m_rigidBody = this.GetComponent<Rigidbody2D>();
		m_animator = this.GetComponent<Animator>();
		isMoving = true;
		m_animator.SetBool("isMoving", isMoving);
	}
	
	// Update is called once per frame
	void Update () {
		handleMove();
	}

	void handleMove () {
		float moveDistance = m_runningSpeed * Time.deltaTime;
		m_rigidBody.MovePosition(new Vector2(transform.position.x + moveDistance, transform.position.y));
		m_animator.SetFloat("input_x", moveDistance > 0 ? 1 : -1);
	}

	void handleJump () {
	}

	void handleCastAbility () {

	}
}
