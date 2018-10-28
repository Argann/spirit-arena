using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damages;

    public void DestroyMe() {
        Destroy(gameObject);
    }

    public void StopMe() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}