using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	// ================================================
	[Header("Game Settings")]
	public string playerPrefix = "P1";
	// ================================================
	[Header("Characteristics")]
	public float movementSpeed = 7.5f;
	public long swapCooldownMs = 50;
    public int lifePoints = 10;
    public GameObject defaultWeapon = null;
	// ================================================
	[Header("Buffs")]
    public int armor = 0;
	public float damageMultiplicator = 1f;
	public float attackSpeedMultiplicator = 1f;
    public float movementSpeedMultiplicator = 1f;
    public float bonusDurationMultiplicator = 1f;
    public float swapCooldownMultiplicator = 1f;
	// ================================================
	[Header("Bonus weapon")]
    public GameObject bonusWeapon = null;
	// ================================================
	[Header("Swap settings")]
    public GameObject bullet = null;
    public GameObject otherPlayer = null;
    private long lastSwapTimingMs = 0;
	// ================================================

	private string horizontalInputLabel;
	private string VerticalInputLabel;
	private string aimHorizontalInputLabel;
	private string aimVerticalInputLabel;
	// private string playerActionLabel;
	private string playerSwapLabel;

	private Vector2 aim = new Vector2(0,0);
    private Vector2 movement = new Vector2(0,0);
    
    // since we use axis (needed for key mapping), we have to
    // detect button press by ourselves...
    private bool previousFrameSwapUp = true;

	void Start () {
		gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        horizontalInputLabel    = string.Concat(playerPrefix, "_Horizontal");
        VerticalInputLabel      = string.Concat(playerPrefix, "_Vertical");
        aimHorizontalInputLabel = string.Concat(playerPrefix, "_aim_horizontal");
        aimVerticalInputLabel   = string.Concat(playerPrefix, "_aim_vertical");
        // playerActionLabel       = string.Concat(playerPrefix, "_action");
        playerSwapLabel         = string.Concat(playerPrefix, "_swap");
	}

    public void TakeDamages(int n)
    {
        lifePoints -= n - armor;
        if (n <= 0)
        {
            Debug.Log("YOU ARE DEAD");
        }
    }

	void Update () {
        // ------ update aim ------
        aim = new Vector2(Input.GetAxis(aimHorizontalInputLabel), Input.GetAxis(aimVerticalInputLabel));
        // ------ update movement ------
        movement = new Vector2(Input.GetAxisRaw(horizontalInputLabel), Input.GetAxisRaw(VerticalInputLabel));
		movement.Normalize();
		movement = movement * movementSpeed * movementSpeedMultiplicator;
        // ------ attacks ------
        if (aim.magnitude >= 0.1f)
        {
		    aim.Normalize();
            if (bonusWeapon)
            {
                bonusWeapon.GetComponent<Weapon>().fire(gameObject);
            }
            else if (defaultWeapon)
            {
                defaultWeapon.GetComponent<Weapon>().fire(gameObject);
            }
            else
            {
                Debug.LogError("no weapon equiped");
            }
        }
        // ------ swaps ------
        bool swapButtonPressed = (Input.GetAxisRaw(playerSwapLabel) != 0);
        long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        if(previousFrameSwapUp && swapButtonPressed && (now >= lastSwapTimingMs + (long)(swapCooldownMultiplicator * swapCooldownMs))) {
            lastSwapTimingMs = now;
            if (otherPlayer)
            {
                PlayerControls P2 = otherPlayer.GetComponent<PlayerControls>();
                GameObject tmp = bullet;
                bullet = P2.bullet;
                P2.bullet = tmp;
            }
            else
            {
                Debug.LogError("other player not set");
            }
        }
        previousFrameSwapUp = !swapButtonPressed;
	}

	void FixedUpdate() {
		gameObject.GetComponent<Rigidbody2D>().velocity = movement;
	}

	public Vector2 GetAim()
	{
		return aim;
	}
}
