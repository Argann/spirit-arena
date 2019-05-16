using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AutoSwitchPlayerHead : MonoBehaviour {

    public int playerNumber;
    public Sprite spritePhysical;
    public Sprite spriteSpirit;

    private bool previousStateSpirit = true;
    private PlayerControls currentPlayer;
    private Image imageUI;

    // Start is called before the first frame update
    void Start() {
        imageUI = GetComponent<Image>();
        currentPlayer = MobManager.GetPlayer(playerNumber).GetComponent<PlayerControls>();
        previousStateSpirit = currentPlayer.IsSpirit;

        imageUI.sprite = currentPlayer.IsSpirit ? spriteSpirit : spritePhysical;


        imageUI = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (currentPlayer.IsSpirit != previousStateSpirit) {
            imageUI.sprite = currentPlayer.IsSpirit ? spriteSpirit : spritePhysical;
            previousStateSpirit = currentPlayer.IsSpirit;
        }
    }
}
