using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	public string playerPrefix;
	public float playerSpeed;
	public GameObject[] spells;
	private Vector2 movement;

	public float attackCooldown = 2;
	public float swapCooldown = 2;
	private float attackStart;
	private float swapStart;
	private int state;

	void Start () {
		gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
		attackStart = -attackCooldown;
		swapStart = -swapCooldown;
		state = 1;
	}

	void Update () {
		string horizontalInputLabel = string.Concat(playerPrefix, "_Horizontal");
		string VerticalInputLabel   = string.Concat(playerPrefix, "_Vertical");
		string PlayerAction = string.Concat(playerPrefix, "_action");
		string PlayerSwap = string.Concat(playerPrefix, "_swap");

		float inputX = Input.GetAxisRaw(horizontalInputLabel);
		float inputY = Input.GetAxisRaw(VerticalInputLabel);
		movement = new Vector2(inputX, inputY);
		movement.Normalize();
		movement = movement * playerSpeed;

		if((Input.GetAxisRaw(PlayerAction) != 0) && (Time.time - attackStart > attackCooldown)) {
			Debug.Log("pew pew");
			attackStart = Time.time;
			GameObject instance = Instantiate(spells[state], gameObject.GetComponent<Transform>().position + new Vector3(0.6f,0f,0f), gameObject.GetComponent<Transform>().rotation);
			instance.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
		}

		if((Input.GetAxisRaw(PlayerSwap) != 0) && (Time.time - swapStart > swapCooldown)) {
			Debug.Log("SWAP");
			swapStart = Time.time;
			state = 1 - state;
		}
	}
	
	void FixedUpdate() {
		gameObject.GetComponent<Rigidbody2D>().velocity = movement;
	}
}
