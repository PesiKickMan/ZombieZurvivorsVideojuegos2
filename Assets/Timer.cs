using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    float remainingTime;

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
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                TriggerGameOver();
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
