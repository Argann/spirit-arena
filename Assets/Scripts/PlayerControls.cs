using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	public string playerPrefix;
	public float playerSpeed;
	private Vector2 movement;

	void Start () {
		gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
	}

	void Update () {
		string horizontalInputLabel = string.Concat(playerPrefix, "_Horizontal");
		string VerticalInputLabel   = string.Concat(playerPrefix, "_Vertical");

		float inputX = Input.GetAxisRaw(horizontalInputLabel);
		float inputY = Input.GetAxisRaw(VerticalInputLabel);
		movement = new Vector2(inputX, inputY);
		movement.Normalize();
		movement = movement * playerSpeed;
	}
	
	void FixedUpdate() {
		gameObject.GetComponent<Rigidbody2D>().velocity = movement;
	}
}
