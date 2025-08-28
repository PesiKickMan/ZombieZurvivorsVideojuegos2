using UnityEngine;

public class ExperienceDna : MonoBehaviour, ICollectible
{
    public int experienceGranted;

    public void Collect()
    {
        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}
