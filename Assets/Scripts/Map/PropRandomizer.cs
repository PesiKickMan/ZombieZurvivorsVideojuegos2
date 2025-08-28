using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public List<GameObject> propSpawnPoints; // Lista de props para randomizar
    public List<GameObject> propPrefabs; // Lista de prefabs de props

    void Start()
    {
        spawnProps();
    }


    void Update()
    {

    }

    void spawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.SetParent(sp.transform); // Establece el spawn point como padre del prop
        }
    }
}
