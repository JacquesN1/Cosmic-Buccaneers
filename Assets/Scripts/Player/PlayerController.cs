using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Sprites/GameObjects
    SpriteRenderer spriteRenderer;
    public GameObject lazer;
    public GameObject explosion;
    public GameObject crosshair;
    public PlayerPopUp popUp;
    public PlayerInventory inventory;
    private Material matDefault;
    private Material matDamage;

    //UI
    public PauseMenu pauseMenu;
    public GameOverMenu gameOverMenu;

    //Controls
    PlayerControls controls;
    Vector2 movementInput;
    public float maxSpeed = 10;
    public float acceleration = 5;
    private Vector3 originalPosition;
    private Vector2 movementDirection;
    private Vector2 movement;
    public bool interact = false;
    public bool isPaused = false;
    float interactCooldown = 0.5f;
    float interactCooldownTimer = 0;

    //Health
    public float maxHealth = 100;
    public float damageCoolDown = 1;
    private float damageCoolDownTimer;
    public float health;
    public bool isDead = false;
    float gameOverTimer = 0;

    //Weapons
    public float fireDelay = 0.5f;
    float fireCooldownTimer = 0;
    public Vector3 lazerOffset = new Vector3(0.44f, 0, 0);
    public bool isAimed = false;
    
    //Level/XP
    public float level = 1;
    public float xp = 0;
    public float maxXP = 500;

    //Wanted Level
    public float wantedLevel = 0;
    public float maxWantedLevel = 5;
    public float wantedLevelProgress = 0;
    public float wantedLevelThreshold = 5;
    public float wantedLevelCooldown = 60;
    public float wantedLevelTimer = 0;
    public bool isWantedOnCooldown = false;

    void Awake()
    {
        //reference needed player materials for damage flash
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventory = GetComponent<PlayerInventory>();
        gameObject.GetComponent<Renderer>().enabled = true;
        matDefault = spriteRenderer.material;
        matDamage = Resources.Load("DamageFlash", typeof(Material)) as Material;

        gameObject.GetComponent<ParticleSystem>().Play();

        //Update health
        health = maxHealth;

        controls = new PlayerControls();

        //Joystick player movement 
        controls.Gameplay.MovePlayer.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
     
        //Player fire controlls
        controls.Gameplay.Fire.performed += ctx => FireWeapons();

        //Player aim controlls
        controls.Gameplay.Aim.performed += ctx => isAimed = true;
        controls.Gameplay.Aim.canceled += ctx => isAimed = false;

        //Player object interaction controlls
        controls.Gameplay.Interact.performed += ctx => Interact(true);
        controls.Gameplay.Interact.canceled += ctx => Interact(false);

        //Pause game controlls
        controls.Gameplay.Pause.performed += ctx => pauseMenu.OpenMenu();
    }

    void Update()
    {
        if (AreControlsActive())
        {
            //Update cooldown timers
            fireCooldownTimer -= Time.deltaTime;
            damageCoolDownTimer -= Time.deltaTime;
            interactCooldownTimer -= Time.deltaTime;
            wantedLevelTimer -= Time.deltaTime;

            //decrese wanted level over time
            if (wantedLevelTimer <= 0)
            {
                isWantedOnCooldown = false;
            }
            if(isWantedOnCooldown == false && (wantedLevel > 0 || (wantedLevel == 0 && (wantedLevelProgress - Time.deltaTime) >= 0)))
            {
                AddWantedLevel(-Time.deltaTime);
            }

            //Update position
            movementDirection = Vector2.Lerp(movementDirection, movementInput, Time.deltaTime * acceleration);
            MoveThePlayer(movementDirection);

            crosshair.GetComponent<Crosshair>().SetIsAimed(isAimed);
            if (isAimed)
            {
                //Rotate to face crosshair
                Vector2 direction = crosshair.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, maxSpeed * Time.deltaTime);
            }
            else if (!isAimed)
            {
                //rotate to face direction moving
                Vector2 moveDirection = transform.position - originalPosition;
                if (moveDirection != Vector2.zero)
                {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, maxSpeed * Time.deltaTime);
                }
            }

            //Update original position
            originalPosition = transform.position;
        }

        if (isDead)
        {
            //Show game over menu if dead for length of game over timer
            gameOverTimer -= Time.deltaTime;
            if (gameOverTimer <= 0 && isDead && gameOverMenu.gameObject.activeSelf == false)
            {
                gameOverMenu.OpenMenu();
            }
        }
    }

    void MoveThePlayer(Vector2 desiredDirection)
    {
        movement = desiredDirection;
        movement = movement * maxSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

    }

    void FireWeapons() 
    {
        //If weapons are not on cooldown, fire
        if (AreControlsActive() && fireCooldownTimer <= 0)
        {
            if (inventory.ReturnItemAmmount("Ammo") > 0)
            {
                Vector3 offset = transform.rotation * lazerOffset;
                Instantiate(lazer, transform.position + offset, transform.rotation);
                fireCooldownTimer = fireDelay;
                inventory.EditItemAmmount("Ammo", -1);
                inventory.weightCarrying -= inventory.ReturnItemWeight("Ammo");
            }
            else
            {
                PopUpText("NO AMMO", 1);
            }
        }
    }

    public void HealDamage()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //Take damage if not on damage cooldown
        if (damageCoolDownTimer <= 0)
        {
            health -= damage;
            damageCoolDownTimer = damageCoolDown;

            //Flash red when damaged
            spriteRenderer.material = matDamage;
            Invoke("ResetMaterial", damageCoolDown);
        }

        //Check health
        if (health <= 0 && !isDead)
        {
            GameOver();
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

    void Interact(bool isInteracting)
    {
        if (isInteracting && AreControlsActive() && interactCooldownTimer <= 0)
        {
            interact = true;
        }
        else 
        {
            interact = false;
            interactCooldownTimer = interactCooldown;
        }
    }

    public void AddXP(float _xp)
    {
        xp += _xp;

        //check level
        if (xp >= maxXP)
        {
            level++;
            maxXP = maxXP * 2;
        }
    }

    public void AddWantedLevel(float _wantedLevelProgress)
    {
        //if incresing wanted level, start cooldown timer
        if (_wantedLevelProgress > 0)
        {
            isWantedOnCooldown = true;
            wantedLevelTimer = wantedLevelCooldown;
        }

        //Make sure wanted level does not become negative
        if (wantedLevel > 0 || (wantedLevel == 0 && (wantedLevelProgress += _wantedLevelProgress) >= 0))
        {
            wantedLevelProgress += _wantedLevelProgress;
        }

        //check level
        if (wantedLevelProgress >= wantedLevelThreshold && wantedLevel < maxWantedLevel)
        {
            wantedLevel++;
            wantedLevelThreshold = wantedLevelThreshold * 2;
            wantedLevelProgress = 0;
        }
        else if (wantedLevelProgress <= 0 && wantedLevel > 0)
        {
            wantedLevel--;
            wantedLevelThreshold = wantedLevelThreshold / 2;
            wantedLevelProgress = wantedLevelThreshold - 1;
        }
    }


    public void GameOver()
    {
        //spawn explosion on death
        Instantiate(explosion, transform.position, transform.rotation);

        //hide player when dead
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();
        isDead = true;

        //set timer to Open Game over manu
        gameOverTimer = 3;
    }

        public void PopUpText(string text, float popUpDuration)
    {
        popUp.displayPopUp(text, popUpDuration);

    }

    public void PausePlayerControls(bool _isPaused)
    {
        isPaused = _isPaused;
    }

    public bool AreControlsActive()
    {
        if (isDead || isPaused)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DayPassed()
    {
        for (int crew = inventory.crew; crew > 0; crew--)
        {
            if (inventory.ReturnItemAmmount("Food") > 0)
            {
                inventory.EditItemAmmount("Food", -1);
                inventory.weightCarrying -= inventory.ReturnItemWeight("Food");
            }
        }
        if(inventory.ReturnItemAmmount("Food") == 0)
        {
            TakeDamage(1);
            PopUpText("WARNING: NO FOOD", 1);
        }

        if (inventory.ReturnItemAmmount("Energy") > 0)
        {
            inventory.EditItemAmmount("Energy", -5);
            inventory.weightCarrying -= (inventory.ReturnItemWeight("Energy") * 5);
        }
        else
        {
            TakeDamage(1);
            PopUpText("WARNING: BACKUP GENERATORS ACTIVATED", 1);
        }
    }

    //Enable/disable controls
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}