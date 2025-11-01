using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    private Slider slider;
    //public CharacterScriptableObject characterData;

    public void ChangeMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(float healthAmount)
    {
        slider.value = healthAmount;
    }

    public void InitializeHealthBar(float healthAmount)
    {
        slider = GetComponent<Slider>();
        ChangeMaxHealth(healthAmount);
        ChangeCurrentHealth(healthAmount);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
