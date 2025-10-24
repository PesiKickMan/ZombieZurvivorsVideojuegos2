using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsPanel;

        void Start()
    {
        // Asegurarse de que el panel de controles esté oculto al inicio
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && controlsPanel != null && controlsPanel.activeSelf)
        {
            HideControlsMenu();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
        public void HideControlsMenu()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    public void ToggleControlsMenu()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(!controlsPanel.activeSelf);
    }
}
