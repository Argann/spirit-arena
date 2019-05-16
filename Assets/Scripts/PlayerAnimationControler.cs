using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerAnimationControler : MonoBehaviour {

    public GameObject physicalObject;

    public GameObject spiritObject;

    private PlayerControls playerControls;
    private bool previousFrameIsSpirit = false;

    // Start is called before the first frame update
    void Start() {
        playerControls = GetComponent<PlayerControls>();
        previousFrameIsSpirit = playerControls.IsSpirit;

        spiritObject.SetActive(previousFrameIsSpirit);
        physicalObject.SetActive(!previousFrameIsSpirit);

    }

    // Update is called once per frame
    void Update() {
        if (previousFrameIsSpirit != playerControls.IsSpirit) {
            previousFrameIsSpirit = playerControls.IsSpirit;
            spiritObject.SetActive(previousFrameIsSpirit);
            physicalObject.SetActive(!previousFrameIsSpirit);

        }
    }
}
