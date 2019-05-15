using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFlipMob : MonoBehaviour {

    private Vector2 previousPosition;

    private bool isFacingLeft = true;

    // Start is called before the first frame update
    void Start() {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        Vector2 currentPosition = transform.position;
        if (previousPosition.x > currentPosition.x && !isFacingLeft) {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFacingLeft = true;
        } else if (previousPosition.x < currentPosition.x && isFacingLeft) {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);                              
            isFacingLeft = false;
        }
        previousPosition = transform.position;
    }
}
