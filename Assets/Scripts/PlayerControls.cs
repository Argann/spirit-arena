using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Input;

public class PlayerControls : MonoBehaviour {
	// ================================================
	[Header("Game Settings")]
	public string playerPrefix = "P1";
    public InputActionAsset iaa = default;
	// ================================================
	[Header("Characteristics")]
	public float movementSpeed = 7.5f;
	public long swapCooldownMs = 50;
    public int lifePoints = 10;
    public int maxLifePoints = 10;
    public GameObject defaultWeapon = null;

    [Header("UI elements")]
    public Slider hpSlider;

    [SerializeField]
    private int points = 0;
    public int Points
    {
        get { return points;}
        set { points = value; if (scoreUI) scoreUI.GetComponent<Text>().text = points.ToString(); }
    }
	// ================================================
	[Header("UI")]
    public GameObject statsUI = null;
    public GameObject scoreUI = null;
    public HPBarManager hpbar;
	// ================================================
	[Header("Buffs")]

    private int armor = 0;
    public int Armor
    {
        get { return armor;}
        set { armor = value; SetStatUI("armor_text", armor, ""); }
    }
    

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
        set { swapCooldownMultiplicator = value; }
    }

    [SerializeField]    
    private int upgradeCount = 0;
    public int UpgradeCount
    {
        get { return upgradeCount; }
        set { upgradeCount = value; }
    }

    public static List<PlayerControls> instances = new List<PlayerControls>();

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
    private static long lastSwapTimingMs = 0;
	// ================================================
    public long lastShotTimingMs = 0;
	// ================================================

	private Vector2 aim = new Vector2(0,0);
    private Vector2 movement = new Vector2(0,0);

    // =================================================
    private Animator animator;
    
    // since we use axis (needed for key mapping), we have to
    // detect button press by ourselves...
    private bool previousFrameSwapUp = true;
    private bool swapButtonPressed = false;

    [SerializeField]
    private bool isSpirit;

    public bool IsSpirit
    {
        get { return isSpirit; }
    }

    [HideInInspector]
    public int minigameScore = 0;
    public Text minigameScoreUI;

    private bool firstFrameDead = true;

    /*
     * Gere l'augmentation des PV max
     */
    public void IncreaseMaxHealth(int inc) {
        maxLifePoints += inc;
        lifePoints = maxLifePoints;
        hpSlider.maxValue = maxLifePoints;
        hpSlider.value = lifePoints;
    }

    public void Awake() {
        var actionControl = iaa.GetActionMap("gameplay");
        InputAction movementAction = actionControl.GetAction("movement");
        movementAction.performed += Move;
        movementAction.cancelled += StopMoving;

        InputAction aimAction = actionControl.GetAction("aim");
        aimAction.performed += Shoot;
        aimAction.cancelled += StopShooting;

        InputAction swapAction = actionControl.GetAction("swap");
        swapAction.performed += Swap;
    }

    public void OnEnable() {
        iaa.Enable();
    }

    public void OnDisable() {
        iaa.Disable();
    }

    /*
     * Appelé à l'instanciation de la classe
     * Associe les controles au joueur correspondant
     */
	void Start () {
        hpSlider.maxValue = maxLifePoints;
        hpSlider.value = lifePoints;
		gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        animator = GetComponent<Animator>();
        hpbar = GetComponent<HPBarManager>();
        sprite = GetComponent<SpriteRenderer>();
        instances.Add(this);
	}

	[Header("Damages")]
    public int takingDamageColorFrames = 10;
    private int takingDamagesFrameCount = 0;

    private SpriteRenderer sprite;
    private Color previousColor = Color.black;

    /*
     * Methode de gestion des degats pris
     */
    public void TakeDamages(int n)
    {
        if (takingDamagesFrameCount == 0)
        {
            lifePoints -= (n - armor < 1) ? 1 : n - armor;
            hpSlider.value = lifePoints;
            takingDamagesFrameCount = takingDamageColorFrames;
            previousColor = sprite.color;
            sprite.color = Color.red;
        }
    }

    /*
     * Update est appelé à chaque frame
     * Ici :
      * Calcule l'angle de tir du personnage
      * Calcule la direction de deplacement du personnage
      * Fait apparaitre le personnage rouge / normal lorsqu'il prend des coups
      * Gere la mort des personnages
      * Gere les animations de personnage
      * Gere le tir des personnages
      * Gere le swap des personnages
     */
	void Update () {
        // ------ update movement ------

        if (takingDamagesFrameCount > 0)
        {
            takingDamagesFrameCount--;
            if (takingDamagesFrameCount == 0)
            {
                sprite.color = previousColor;
            }
        }

        if (lifePoints <= 0) {
            lifePoints = 0;
            if (firstFrameDead) {
                firstFrameDead = false;
                points /= 2;
                SoundManager.PlaySoundDeath();
            }
            animator.SetBool("Dead", true);
            animator.SetBool("Running", false);
            movement = Vector2.zero;
        } else {
            firstFrameDead = true;
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
            swapButtonPressed = false;
        }
	}

    /*
     * FixedUpdate est appelé à chaque intervale constant (0.02s par defaut)
     * Ici, applique le mouvement au joueur
     */
	void FixedUpdate() {
		gameObject.GetComponent<Rigidbody2D>().velocity = movement;
	}

    /*
     * Retourne la direction de tir du joueur
     */
	public Vector2 GetAim()
	{
		return aim;
	}

    /*
     * Applique un recul sur un personnage joueur touché
     */
    public void Recoil(Vector3 recoilDirection) {
        begin = Time.time;
        movement = recoilDirection;
    }

    /*
     * Verifie si tous les joueurs sont morts
      * false si au moins un joueur est vivant
      * true sinon
     */
    public static bool areDead() {
        bool result = false;
        foreach (PlayerControls control in instances) {
            result = result || control.lifePoints > 0;
        }
        return !result;
    }

    public void Move(InputAction.CallbackContext context) {
        if (Time.time - begin > 0.5f && lifePoints > 0) {
            movement = context.ReadValue<Vector2>();
            movement.Normalize();
            movement = movement * movementSpeed * movementSpeedMultiplicator;
        }
    }

    public void StopMoving(InputAction.CallbackContext context) {
        movement = Vector2.zero;
    }

    public void Shoot(InputAction.CallbackContext context) {
        aim = context.ReadValue<Vector2>();
    }

    public void StopShooting(InputAction.CallbackContext context) {
        aim = Vector2.zero;
    }

    public void Swap(InputAction.CallbackContext context) {
        swapButtonPressed = true;
    }

    public void IncreasePlayerCpt(InputAction.CallbackContext context) {
		minigameScore++;
		minigameScoreUI.text = "" + minigameScore;
		SoundManager.PlaySoundMinigameHit();
	}
}
