using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float fadeDuration = 1.0f;
    
    
    [Header("Win Condition")]
    public int winCondition;
    public EnemySpawner spawner;

    [Header("Game Over UI")]
    [SerializeField] 
    CanvasGroup looseFadeCanvasGroup; 
    [SerializeField] 
    TextMeshProUGUI gameOverText;
    [SerializeField]
    GameObject looseUI;

    [Header("Win UI")]
    [SerializeField]
    TextMeshProUGUI winText;
    [SerializeField]
    CanvasGroup winFadeCanvasGroup;
    [SerializeField]
    GameObject winUI;

    [Header("Pause Menu UI")]
    [SerializeField]
    private GameObject pauseMenu;

    bool gameOverTriggered = false;
    bool winTriggered = false;
    public bool levelUpTriggered = false;

    void Start()
    {
        // Asegurarse estado inicial UI
        if (looseFadeCanvasGroup != null)
        {
            looseFadeCanvasGroup.alpha = 0f;
        }
        
        if (looseUI != null)
            looseUI.SetActive(false);

        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (winFadeCanvasGroup != null)
        {
            winFadeCanvasGroup.alpha = 0f;
        }
     
        if (winUI != null)
            winUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1f)
            {
                Pause();
            }
            else
            {
                if (!gameOverTriggered && !winTriggered && !levelUpTriggered)
                    Resume();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        if (spawner.cantidadMuertes >= winCondition)
        {
            TriggerWin();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;  // Asegurarse que el tiempo está normal antes de reiniciar
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(obj);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title Screen");
    }
    
    public void TriggerGameOver()
    {
        SoundManager.instance.PlaySFX(SoundManager.instance.playerDeath);
        if (gameOverTriggered) return;
        gameOverTriggered = true;
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        if(looseUI != null)
            looseUI.SetActive(true);
        
        // Mostrar y hacer fade in usando unscaledDeltaTime para que funcione aun si pausas el juego
        if (looseFadeCanvasGroup != null)
        {
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                looseFadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / Mathf.Max(0.0001f, fadeDuration));
                yield return null;
            }

            looseFadeCanvasGroup.alpha = 1f;
        }

        Time.timeScale = 0f;
    }

    public void TriggerWin()
    {
        SoundManager.instance.PlaySFX(SoundManager.instance.success);
        if (winTriggered) return;
        winTriggered = true;
        StartCoroutine(WinSequence());
    }
    
    IEnumerator WinSequence()
    {
        if(winUI != null)
        winUI.SetActive(true);
        
        if (winFadeCanvasGroup != null)
        {
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                winFadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / Mathf.Max(0.0001f, fadeDuration));
                yield return null;
            }

            winFadeCanvasGroup.alpha = 1f;
        }

        Time.timeScale = 0f;
    }
}
