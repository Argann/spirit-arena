using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerAnimationControler : MonoBehaviour {

    public GameObject physicalObject;

    public GameObject spiritObject;

    private PlayerControls playerControls;
    private bool previousFrameIsSpirit = false;

    private Animator physicalAnimator;
    private float previousHealth;

    // Start is called before the first frame update
    void Start() {
        playerControls = GetComponent<PlayerControls>();
        previousFrameIsSpirit = playerControls.IsSpirit;

        spiritObject.SetActive(previousFrameIsSpirit);
        physicalObject.SetActive(!previousFrameIsSpirit);

        physicalAnimator = physicalObject.GetComponent<Animator>();

        previousHealth = playerControls.lifePoints;
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

        physicalAnimator.SetBool("Dead", playerControls.lifePoints == 0);


        previousHealth = playerControls.lifePoints;
    }
}
