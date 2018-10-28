using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	[Header("UI")]
    public GameObject statsUI = null;
	// ================================================
	[Header("Buffs")]
    public int armor = 0;

    [SerializeField]
    private float damageMultiplicator = 1f;
    public float DamageMultiplicator
    {
        get { return damageMultiplicator;}
        set { damageMultiplicator = value; SetStatUI("damage_text", (int)(damageMultiplicator * 100), "%"); }
    }

    [SerializeField]
    private float attackSpeedMultiplicator = 1f;
    public float AttackSpeedMultiplicator
    {
        get { return attackSpeedMultiplicator;}
        set { attackSpeedMultiplicator = value; SetStatUI("attack-speed_text", (int)(AttackSpeedMultiplicator * 100), "%"); }
    }
    
    [SerializeField]    
    private float movementSpeedMultiplicator = 1f;
    public float MovementSpeedMultiplicator
    {
        get { return movementSpeedMultiplicator;}
        set { movementSpeedMultiplicator = value; SetStatUI("move_text", (int)(MovementSpeedMultiplicator * 100), "%"); }
    }
    
    [SerializeField]
    private float bonusDurationMultiplicator = 1f;
    public float BonusDurationMultiplicator
    {
        get { return bonusDurationMultiplicator;}
        set { bonusDurationMultiplicator = value; SetStatUI("time_text", (int)(BonusDurationMultiplicator * 100), "%"); }
    }

    [SerializeField]
    private float swapCooldownMultiplicator = 1f;
    public float SwapCooldownMultiplicator
    {
        get { return swapCooldownMultiplicator;}
        set { swapCooldownMultiplicator = value; /* SetStatUI("damage_text", (int)(damageMultiplicator * 100), "%"); */ }
    }

    private float begin = -1f;

    private void SetStatUI(string label, int value, string suffix)
    {
        if (statsUI)
        {
            statsUI.transform.Find(label).GetComponent<Text>().text = value + suffix;
        }
        else
        {
            Debug.LogError("no stat UI set");
        }
    }
    
	// ================================================
	[Header("Bonus weapon")]
    public GameObject bonusWeapon = null;
	// ================================================
	[Header("Swap settings")]
    public GameObject bullet = null;
    public GameObject otherPlayer = null;
    private long lastSwapTimingMs = 0;
	// ================================================
    public long lastShotTimingMs = 0;
	// ================================================

	private string horizontalInputLabel;
	private string VerticalInputLabel;
	private string aimHorizontalInputLabel;
	private string aimVerticalInputLabel;
	// private string playerActionLabel;
	private string playerSwapLabel;

	private Vector2 aim = new Vector2(0,0);
    private Vector2 movement = new Vector2(0,0);

    // =================================================
    private Animator animator;
    
    // since we use axis (needed for key mapping), we have to
    // detect button press by ourselves...
    private bool previousFrameSwapUp = true;

    [SerializeField]
    private bool isSpirit;

	void Start () {
		gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        horizontalInputLabel    = string.Concat(playerPrefix, "_Horizontal");
        VerticalInputLabel      = string.Concat(playerPrefix, "_Vertical");
        aimHorizontalInputLabel = string.Concat(playerPrefix, "_aim_horizontal");
        aimVerticalInputLabel   = string.Concat(playerPrefix, "_aim_vertical");
        // playerActionLabel       = string.Concat(playerPrefix, "_action");
        playerSwapLabel         = string.Concat(playerPrefix, "_swap");
        animator = GetComponent<Animator>();
        BonusDurationMultiplicator = 2f;
	}

    public void TakeDamages(int n)
    {
        lifePoints -= n - armor;
    }

	void Update () {
        // ------ update aim ------
        aim = new Vector2(Input.GetAxis(aimHorizontalInputLabel), Input.GetAxis(aimVerticalInputLabel));
        // ------ update movement ------
        if (Time.time - begin > 0.5f) {
            movement = new Vector2(Input.GetAxisRaw(horizontalInputLabel), Input.GetAxisRaw(VerticalInputLabel));
            movement.Normalize();
            movement = movement * movementSpeed * movementSpeedMultiplicator;
        }

        if (lifePoints == 0) {
            animator.SetBool("Dead", true);
            animator.SetBool("Running", false);
            movement = Vector2.zero;
        } else if (lifePoints < 0) {
            lifePoints = 0;
        } else {
            animator.SetBool("Dead", false);

            if (movement.Equals(Vector2.zero)) {
                animator.SetBool("Running", false);
            } else {
                animator.SetBool("Running", true);
            }

            if(movement.x > 0) {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
            } else if (movement.x < 0) {
                transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
            }

            if (isSpirit) {
                animator.SetBool("Spirit", true);
            } else {
                animator.SetBool("Spirit", false);            
            }

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
                isSpirit = !isSpirit;
                if (otherPlayer)
                {
                    PlayerControls P2 = otherPlayer.GetComponent<PlayerControls>();
                    GameObject tmp = bullet;
                    bullet = P2.bullet;
                    P2.bullet = tmp;
                    P2.isSpirit = !P2.isSpirit;
                }
                else
                {
                    Debug.LogError("other player not set");
                }
            }
            previousFrameSwapUp = !swapButtonPressed;
        }
	}

	void FixedUpdate() {
		gameObject.GetComponent<Rigidbody2D>().velocity = movement;
	}

	public Vector2 GetAim()
	{
		return aim;
	}

    public void Recoil(Vector3 recoilDirection) {
        begin = Time.time;
        movement = recoilDirection;
    }
}
