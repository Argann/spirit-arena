using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsBehaviourIntro : MonoBehaviour {
	public float apparitionTime;
	public float apparitionSpeed;
	public float firstMoveTime;
	public float firstStopTime;
	public float secondMoveTime;
	public float secondStopTime;
	public float moveSpeed;

	public Vector2 direction;

	public GameObject blurp;


	private Sprite spr;

	// Use this for initialization
	void Start () {
		spr = gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameObject.GetComponent<MobCollider>().isDead) {
			Color col = gameObject.GetComponent<SpriteRenderer>().color;
			if (Time.timeSinceLevelLoad > apparitionTime &&  col.a < 1f) {
				col.a += apparitionSpeed;
				gameObject.GetComponent<SpriteRenderer>().color = col;
			}
			if (Time.timeSinceLevelLoad > firstMoveTime && Time.timeSinceLevelLoad < firstStopTime) {
				gameObject.GetComponent<Animator>().enabled = true;
				transform.Translate(moveSpeed * direction);
			}
			if (Time.timeSinceLevelLoad > firstStopTime && Time.timeSinceLevelLoad < secondMoveTime) {
				gameObject.GetComponent<Animator>().enabled = false;
				gameObject.GetComponent<SpriteRenderer>().sprite = spr;
			}
			if (Time.timeSinceLevelLoad > secondMoveTime && Time.timeSinceLevelLoad < secondStopTime) {
				gameObject.GetComponent<Animator>().enabled = true;
				transform.Translate(moveSpeed * direction);
			}
		} else {
			gameObject.GetComponent<Animator>().enabled = true;
			Instantiate(blurp, transform.position, Quaternion.identity);
			GetComponent<Animator>().SetBool("Dead", true);
			Destroy(gameObject, 0f);
		}
	}
}
