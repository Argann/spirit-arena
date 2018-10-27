using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeout : MonoBehaviour {
	public float ttlMillis;
	private long startTime;

	void Start () {
		startTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
	}
	
	void Update () {
		if (System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond >= startTime + ttlMillis)
		{
			Object.Destroy(this.gameObject);
		}
	}
}
