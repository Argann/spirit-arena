using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour {
	public string prefix;

	public GameObject emptyHP;
	public GameObject fullHP;

	public Sprite lifeBlock;
	public Sprite lifeBlockFull;

	public int currentHP = 10;
	public int maxHP = 10;
	public int baseHP = 10;

	private int previousHP;
	private int previousMaxHP;

	private float ratio;
	private float angle;
	private float currentAngle;
	private GameObject[] instances;

	private Vector3 positionHPBar;

	// Use this for initialization
	void Start () {
		if (prefix == "P1")
			positionHPBar = new Vector3(-7.5f,-4f,0f);
		if (prefix == "P2")
			positionHPBar = new Vector3(7.5f,-4f,0f);

		float fbaseHP = (float)baseHP;
		float fmaxHP = (float)maxHP;
		ratio = 2f * fbaseHP / fmaxHP;
		angle = 18 * ratio;
		currentAngle = 0;
		instances = new GameObject[maxHP];

		for (int i=0; i<maxHP; i++) {
			instances[i] = Instantiate(emptyHP, positionHPBar, Quaternion.identity);
			instances[i].transform.Rotate(new Vector3(0f,0f,currentAngle - i*angle));
			Vector3 scale = instances[i].transform.localScale;
			scale.x = ratio;
			instances[i].transform.localScale = scale;
		}
		
		for (int i=0; i<currentHP; i++) {
			instances[i].GetComponent<SpriteRenderer>().sprite = lifeBlockFull;
		}

		previousHP = currentHP;
		previousMaxHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		currentHP = gameObject.GetComponent<PlayerControls>().lifePoints;

		if (maxHP < baseHP) maxHP = baseHP;
		if(previousMaxHP < maxHP) {
			float fbaseHP = (float)baseHP;
			float fmaxHP = (float)maxHP;
			ratio = 2f * fbaseHP / fmaxHP;
			angle = 18 * ratio;
			currentAngle = 0;

			GameObject[] tmpInstances = new GameObject[maxHP];

			for (int i=0; i<previousMaxHP; i++) {
				tmpInstances[i] = instances[i];
				tmpInstances[i].transform.rotation = Quaternion.identity;
				tmpInstances[i].transform.Rotate(new Vector3(0f,0f,currentAngle - i*angle));
				Vector3 scale = tmpInstances[i].transform.localScale;
				scale.x = ratio;
				tmpInstances[i].transform.localScale = scale;
			}

			for (int i=previousMaxHP; i<maxHP; i++) {
				tmpInstances[i] = Instantiate(emptyHP, positionHPBar, Quaternion.identity);
				tmpInstances[i].transform.Rotate(new Vector3(0f,0f,currentAngle - i*angle));
				Vector3 scale = tmpInstances[i].transform.localScale;
				scale.x = ratio;
				tmpInstances[i].transform.localScale = scale;
			}
			instances = tmpInstances;
			previousMaxHP = maxHP;
			currentHP = maxHP;
		}

		if(previousMaxHP > maxHP) {
			float fbaseHP = (float)baseHP;
			float fmaxHP = (float)maxHP;
			ratio = 2f * fbaseHP / fmaxHP;
			angle = 18 * ratio;
			currentAngle = 0;

			GameObject[] tmpInstances = new GameObject[maxHP];

			for (int i=0; i<maxHP; i++) {
				tmpInstances[i] = instances[i];
				tmpInstances[i].transform.rotation = Quaternion.identity;
				tmpInstances[i].transform.Rotate(new Vector3(0f,0f,currentAngle - i*angle));
				Vector3 scale = tmpInstances[i].transform.localScale;
				scale.x = ratio;
				tmpInstances[i].transform.localScale = scale;
			}

			for (int i=maxHP; i<previousMaxHP; i++) {
				Destroy(instances[i].gameObject, 0f);
			}
			instances = tmpInstances;
			previousMaxHP = maxHP;
		}
		
		if (currentHP < 0) currentHP = 0;
		if (currentHP > maxHP) currentHP = maxHP;

		if(previousHP > currentHP && previousHP <= maxHP) {
			for (int i=currentHP; i<previousHP; i++) {
				instances[i].GetComponent<SpriteRenderer>().sprite = lifeBlock;
			}
		} else if (previousHP < currentHP) {
			for (int i=previousHP; i<currentHP; i++) {
				instances[i].GetComponent<SpriteRenderer>().sprite = lifeBlockFull;
			}
		}
		previousHP = currentHP;
	}
}
