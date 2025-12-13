using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Stats actuales
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    //Contador de muertes
    public int cantidadMuertes = 0;

    //Cooldown para daño
    public float damageCooldown = 0.5f;
    private float lastDamageTime;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX(SoundManager.instance.enemyDeath);
            
            Kill();
        }
    }

    public void Kill()
    {
        cantidadMuertes++;
        EnemySpawner es = FindFirstObjectByType<EnemySpawner>();
        DropRateManager dropRateManager = FindFirstObjectByType<DropRateManager>();
        dropRateManager.DropItem(transform.position);
        es.OnEnemyKilled();
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime > damageCooldown)
            {
                PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
                player.TakeDamage(currentDamage);
                lastDamageTime = Time.time;
            }
        }
    }
}
