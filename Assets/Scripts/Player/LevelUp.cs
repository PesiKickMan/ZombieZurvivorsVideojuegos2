using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    private PlayerStats playerStats;
    
    private string[] posibilidades = { "Might", "ProjectileSpeed", "Health", "MovementSpeed" };

    [Header("Botones")]
    public Button boton1;
    public Button boton2;
    public Button boton3;
    private Button[] botones;
    
    public GameObject levelUpUI;

    [SerializeField] private GameManager gameManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = FindFirstObjectByType<PlayerStats>();
        botones = new Button[] { boton1, boton2, boton3 };
    }

    public void AplicarMejora()
    {
        int iterator = 3;
        
        while (iterator > 0)
        {
            int index = Random.Range(0, posibilidades.Length);
            string mejora = posibilidades[index];

            switch (mejora)
            {
                case "Might":
                    botones[iterator - 1].GetComponentInChildren<TextMeshProUGUI>().text = "++ Daño Pistola";
                    break;
                case "ProjectileSpeed":
                    botones[iterator - 1].GetComponentInChildren<TextMeshProUGUI>().text = "++ Velocidad Pistola";
                    break;
                case "Health":
                    botones[iterator - 1].GetComponentInChildren<TextMeshProUGUI>().text = "++ Salud Máxima";
                    break;
                case "MovementSpeed":
                    botones[iterator - 1].GetComponentInChildren<TextMeshProUGUI>().text = "++ Velocidad";
                    break;
            }

            iterator--;
        }
       
    }

    public void SeleccionarMejora(Button boton){
        gameManager.levelUpTriggered = false;
        string texto = boton.GetComponentInChildren<TextMeshProUGUI>().text;

        switch (texto)
        {
            case "++ Daño Pistola":
                playerStats.currentMight += 0.5f; // Ajusta el incremento según necesites
                Debug.Log("Might aumentado a: " + playerStats.currentMight);
                break;
            case "++ Velocidad Pistola":
                playerStats.currentProjectileSpeed += 0.5f; // Ajusta el incremento según necesites
                Debug.Log("Projectile Speed aumentado a: " + playerStats.currentProjectileSpeed);
                break;
            case "++ Salud Máxima":
                playerStats.maxHealth += 10f; // Ajusta el incremento según necesites
                playerStats.currentHealth += 10f; // También aumenta la salud actual
                HealthBar.Instance.InitializeHealthBar(playerStats.currentHealth);
                Debug.Log("Health aumentado a: " + playerStats.maxHealth);
                break;
            case "++ Velocidad":
                playerStats.currentMoveSpeed += 0.5f; // Ajusta el incremento según necesites
                Debug.Log("Movement Speed aumentado a: " + playerStats.currentMoveSpeed);
                break;
        }

        Time.timeScale = 1f; // Reanuda el juego
        levelUpUI.SetActive(false);
    }

}
