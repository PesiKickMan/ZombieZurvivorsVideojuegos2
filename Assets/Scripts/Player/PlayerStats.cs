using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //Stats Actuales
    //public float currentHealth;
    public float currentRecovery;
    public float currentMoveSpeed;
    public float currentMight;
    public float currentProjectileSpeed;

    //Experiencia y Nivel
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;
    public LevelUp levelUpManager;
    public GameObject levelUpUI;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    [Header("Health")]
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] public HealthBar healthBar;

    public GameManager gameManager;


    private void Start()
    {        
        maxHealth = characterData.MaxHealth;
        currentHealth = maxHealth;
        HealthBar.Instance.InitializeHealthBar(currentHealth);
    }

    void Awake()
    {
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;  
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUp();
    }

    public void LevelUp()
    {
        if (experience >= experienceCap) //Sube de nivel si la experiencia es mayor o igual a la cantidad necesaria para subir de nivel
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
            
            if(levelUpUI != null){
                Time.timeScale = 0f; // Pausa el juego
                levelUpUI.SetActive(true);
            }

            if(SoundManager.instance != null)
                SoundManager.instance.PlaySFX(SoundManager.instance.levelUp);
            
            levelUpManager.AplicarMejora();
            gameManager.levelUpTriggered = true;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                gameManager.TriggerGameOver();
            }
            HealthBar.Instance.ChangeCurrentHealth(currentHealth);
        }
       
    }
    
}
