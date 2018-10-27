using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	public string playerPrefix;
	public float playerSpeed;
	public GameObject[] spells;
	private Vector2 movement;

	public float attackCooldown = 1;
	public float attackSpeed = 10;
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
		string aimHorizontalInputLabel = string.Concat(playerPrefix, "_aim_horizontal");
		string aimVerticalInputLabel   = string.Concat(playerPrefix, "_aim_vertical");
		string PlayerAction = string.Concat(playerPrefix, "_action");
		string PlayerSwap = string.Concat(playerPrefix, "_swap");

		movement = new Vector2(Input.GetAxisRaw(horizontalInputLabel), Input.GetAxisRaw(VerticalInputLabel));
		movement.Normalize();
		movement = movement * playerSpeed;

		Vector2 aim = new Vector2(Input.GetAxis(aimHorizontalInputLabel), Input.GetAxis(aimVerticalInputLabel));
		aim.Normalize();

		if((Input.GetAxisRaw(PlayerAction) != 0) && (Time.time - attackStart > attackCooldown)) {
			attackStart = Time.time;
			GameObject instance = Instantiate(spells[state], transform.position, transform.rotation);
			instance.GetComponent<Rigidbody2D>().velocity = aim * attackSpeed;
		}

		if((Input.GetAxisRaw(PlayerSwap) != 0) && (Time.time - swapStart > swapCooldown)) {
			swapStart = Time.time;
			state = 1 - state;
		}
	}
	
	void FixedUpdate() {
		gameObject.GetComponent<Rigidbody2D>().velocity = movement;
	}
}
