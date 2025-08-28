using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    /*private void OnDestroy()
    {
        DropItem();
    }*/

    public void DropItem(Vector3 position)
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> posibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                posibleDrops.Add(rate);
            }
        }

        if (posibleDrops.Count > 0)
        {
            Drops drop = posibleDrops[UnityEngine.Random.Range(0, posibleDrops.Count)];
            Instantiate(drop.itemPrefab, position, Quaternion.identity);
        }
    }
}
