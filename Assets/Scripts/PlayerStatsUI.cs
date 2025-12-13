using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;
    //public EnemyStats enemyStats;
    public TextMeshProUGUI statsText;
    public EnemySpawner spawner;
    public GameManager gameManager;

    // Lista de nombres de escenas donde ocultar el apartado de muertes
    public string[] escenasOcultarMuertes = new string[] {"LabLevel"};

    void Update()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        bool ocultarMuertes = System.Array.Exists(escenasOcultarMuertes, escena => escena == escenaActual);

        statsText.text =
            //$"Vida: {playerStats.currentHealth}\n" +
            $"Experiencia: {playerStats.experience}\n" +
            $"Nivel: {playerStats.level}\n" +
            (ocultarMuertes ? "" : $"Muertes: {spawner.cantidadMuertes}/{gameManager.winCondition}");
    }
}
