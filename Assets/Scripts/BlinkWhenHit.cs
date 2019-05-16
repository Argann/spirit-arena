using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkWhenHit : MonoBehaviour {

    public List<Animator> animators = new List<Animator>();
    
    private MobCollider mobCollider;

    private float previousLifePoints;

    void Start() {
        mobCollider = GetComponent<MobCollider>();

        previousLifePoints = mobCollider.lifePoints;
    }

    // Update is called once per frame
    void Update() {
        if (previousLifePoints > mobCollider.lifePoints) {
            previousLifePoints = mobCollider.lifePoints;
            foreach (Animator anim in animators) {
                anim.SetTrigger("Hit");
            }
        }
    }
}
