using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Win Condition")]
    [SerializeField]
    int winCondition;
    public EnemySpawner spawner;

    [Header("Game Over UI")]
    [SerializeField] 
    CanvasGroup fadeCanvasGroup; 
    [SerializeField] 
    TextMeshProUGUI gameOverText;
    [SerializeField]
    float fadeDuration = 1.0f;

    bool gameOverTriggered = false;

    void Start()
    {
        // Asegurarse estado inicial UI
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
            fadeCanvasGroup.interactable = false;
        }

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
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
                if(!gameOverTriggered)
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
            SceneManager.LoadScene("Title Screen");
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
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
        Application.Quit();
    }
    
    public void TriggerGameOver()
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
