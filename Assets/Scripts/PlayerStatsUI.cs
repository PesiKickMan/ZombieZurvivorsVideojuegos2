using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;
    //public EnemyStats enemyStats;
    public TextMeshProUGUI statsText;
    public EnemySpawner spawner;

    void Update()
    {
        statsText.text =
            //$"Vida: {playerStats.currentHealth}\n" +
            $"Experiencia: {playerStats.experience}\n" +
            $"Nivel: {playerStats.level}\n" +
            $"Muertes: {spawner.cantidadMuertes}";
    }
}
