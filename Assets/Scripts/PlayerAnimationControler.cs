using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerAnimationControler : MonoBehaviour {

    public GameObject physicalObject;

    public GameObject spiritObject;

    private PlayerControls playerControls;
    private bool previousFrameIsSpirit = false;

    private Rigidbody2D rb;
    private Animator physicalAnimator;
    private Animator spiritAnimator;
    private float previousHealth;

    // Start is called before the first frame update
    void Start() {
        playerControls = GetComponent<PlayerControls>();
        previousFrameIsSpirit = playerControls.IsSpirit;

        spiritObject.SetActive(previousFrameIsSpirit);
        physicalObject.SetActive(!previousFrameIsSpirit);

        physicalAnimator = physicalObject.GetComponent<Animator>();
        spiritAnimator = spiritObject.GetComponent<Animator>();

        previousHealth = playerControls.lifePoints;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (previousFrameIsSpirit != playerControls.IsSpirit) {
            previousFrameIsSpirit = playerControls.IsSpirit;
            spiritObject.SetActive(previousFrameIsSpirit);
            physicalObject.SetActive(!previousFrameIsSpirit);
        }

        if (previousHealth > playerControls.lifePoints) {
            physicalAnimator.SetTrigger("Hit");
        }

        physicalAnimator.SetBool("Idle", rb.velocity == Vector2.zero);

        physicalAnimator.SetBool("Dead", playerControls.lifePoints == 0);

        spiritAnimator.SetBool("Dead", playerControls.lifePoints == 0);

        previousHealth = playerControls.lifePoints;
    }
}
