using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //Lista de grupos de enemigos que aparecerán en la oleada
        public int waveQuota; //Cantidad total de enemigos que aparecerán en la oleada
        public float spawnInterval; //Tiempo entre la aparición de cada enemigo
        public int spawnCount; //Cantidad de enemigos que han aparecido en la oleada
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;  //Cantidad de enemigos que aparecerán en la oleada
        public int spawnCount; //Cantidad de enemigos de este tipo que han aparecido en la oleada
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //Lista de oleadas
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float spawnTimer; //Timer para determinar cuando aparecerá el siguiente enemigo
    public int enemiesAlive;
    public int maxEnemiesAllowed; //Cantidad máxima de enemigos que pueden aparecer en la oleada
    public bool maxEnemiesReached; //Indica si se alcanzó la cantidad máxima de enemigos permitidos
    public float waveInterval; //Tiempo entre oleadas

    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPositions; //Posiciones relativas al jugador donde pueden aparecer los enemigos

    Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        //Checkeo si es momento de spawnear un nuevo enemigo
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f; //Resetear el timer
            SpawnnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.LogWarning("Current Wave Quota: " + currentWaveQuota);
    }

    void SpawnnEnemies()
    {
        //Me fijo si aparecio la cantidad minima de enemigos de la oleada
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawneo cada tipo de enemigos hasta llegar a la cuota
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Me fijo si la cantidad de enemigos de este tipo aparecieron en la oleada
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return; //No spawneo más enemigos si se alcanzó el máximo permitido
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPositions[Random.Range(0, relativeSpawnPositions.Count)].position, Quaternion.identity);

                    //Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y + Random.Range(-10f, 10f));
                    //Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false; //Reseteo el flag si la cantidad de enemigos vivos es menor al máximo permitido
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
