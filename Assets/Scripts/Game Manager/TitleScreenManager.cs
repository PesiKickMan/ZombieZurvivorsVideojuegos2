using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsPanel;
    [SerializeField]
    private GameObject levelSelectPanel;
    [SerializeField]
    private GameObject level2Button;

    void Start()
    {
        // Asegurarse de que el juego no esté pausado al iniciar el título
        Time.timeScale = 1f;
        
        // Asegurarse de que el panel de controles esté oculto al inicio
        if (controlsPanel != null)
            controlsPanel.SetActive(false);

        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && controlsPanel != null && controlsPanel.activeSelf)
        {
            HideControlsMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && levelSelectPanel != null && levelSelectPanel.activeSelf)
        {
            HideLevelSelectMenu();
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

    public void ToggleLevelSelectMenu()
    {
        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(!levelSelectPanel.activeSelf);
        
        if (!DesbloquearNivel.nivel2Desbloqueado)
        {
            level2Button.SetActive(false);
        }
    }

    public void HideLevelSelectMenu()
    {        
        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
