using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI statsText;

    void Update()
    {
        statsText.text = 
            $"Vida: {playerStats.currentHealth}\n" +
            $"Experiencia: {playerStats.experience}\n" +
            $"Nivel: {playerStats.level}";
    }
}
