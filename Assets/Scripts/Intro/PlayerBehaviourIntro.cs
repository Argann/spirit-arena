using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourIntro : MonoBehaviour {
	public float firstMoveTime;
	public float firstStopTime;
	public float secondMoveTime;
	public float secondStopTime;
	public float thirdMoveTime;
	public float thirdStopTime;
	public float moveSpeed;
	
	public float firstShootTime;
	public float firstStopShootTime;
	public float secondShootTime;
	public float secondStopShootTime;
	private float thirdShootTime;
	private float thirdStopShootTime;

	public Vector2 firstShootDirection;
	public Vector2 secondShootDirection;
	public Vector2 thirdShootDirection;

	public float shootCD;
	private float now;

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		now = Time.timeSinceLevelLoad;
		thirdShootTime = thirdMoveTime;
		thirdStopShootTime = thirdStopTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > firstMoveTime && Time.timeSinceLevelLoad < firstStopTime) {
			transform.Translate(new Vector2(-moveSpeed, 0f));
		}
		if (Time.timeSinceLevelLoad > secondMoveTime && Time.timeSinceLevelLoad < secondStopTime) {
			transform.Translate(new Vector2(-moveSpeed, 0f));
		}
		if (Time.timeSinceLevelLoad > firstShootTime && Time.timeSinceLevelLoad < firstStopShootTime && (Time.timeSinceLevelLoad - now) > shootCD) {
			now = Time.timeSinceLevelLoad;
			gameObject
				.GetComponent<PlayerControls>()
				.defaultWeapon
				.GetComponent<Weapon>()
				.createSingleBullet(bullet, transform.position, firstShootDirection, gameObject.GetComponent<PlayerControls>());
		}
		if (Time.timeSinceLevelLoad > secondShootTime && Time.timeSinceLevelLoad < secondStopShootTime && (Time.timeSinceLevelLoad - now) > shootCD) {
			now = Time.timeSinceLevelLoad;
			gameObject
				.GetComponent<PlayerControls>()
				.defaultWeapon
				.GetComponent<Weapon>()
				.createSingleBullet(bullet, transform.position, secondShootDirection, gameObject.GetComponent<PlayerControls>());
		}
		if (Time.timeSinceLevelLoad > thirdMoveTime && Time.timeSinceLevelLoad < thirdStopTime) {
			transform.Translate(moveSpeed * new Vector2(-.15f, -.7f));
		}
		if (Time.timeSinceLevelLoad > thirdShootTime && Time.timeSinceLevelLoad < thirdStopShootTime && (Time.timeSinceLevelLoad - now) > shootCD) {
			now = Time.timeSinceLevelLoad;
			gameObject
				.GetComponent<PlayerControls>()
				.defaultWeapon
				.GetComponent<Weapon>()
				.createSingleBullet(bullet, transform.position, thirdShootDirection, gameObject.GetComponent<PlayerControls>());
		}
	}
}
