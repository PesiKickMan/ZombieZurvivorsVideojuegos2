using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //Stats Actuales
    //public float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    //Experiencia y Nivel
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    [Header("Health")]
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] public HealthBar healthBar;

    [Header("Game Over UI")]
    [SerializeField] 
    CanvasGroup fadeCanvasGroup; 
    [SerializeField] 
    TextMeshProUGUI gameOverText;
    [SerializeField]
    float fadeDuration = 1.0f;

    bool gameOverTriggered = false;

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
            fadeCanvasGroup.interactable = false;
        }

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
        
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
                TriggerGameOver();
                //SceneManager.LoadScene(1);
            }
            HealthBar.Instance.ChangeCurrentHealth(currentHealth);
        }
       
    }
    
        void TriggerGameOver()
    {
        if (gameOverTriggered) return;
        gameOverTriggered = true;
        StartCoroutine(GameOverSequence());
    }
    
    IEnumerator GameOverSequence()
    {
        // Mostrar y hacer fade in usando unscaledDeltaTime para que funcione aun si pausas el juego
        if (fadeCanvasGroup != null)
        {
            float elapsed = 0f;
            fadeCanvasGroup.blocksRaycasts = true; // bloquear input
            fadeCanvasGroup.interactable = false;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / Mathf.Max(0.0001f, fadeDuration));
                yield return null;
            }

            fadeCanvasGroup.alpha = 1f;
        }

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(true);

        // Pausar el juego (opcional). Si no quieres pausar, comenta la línea siguiente.
        Time.timeScale = 0f;
    }
}
