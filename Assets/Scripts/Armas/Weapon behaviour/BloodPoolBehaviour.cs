using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPoolBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemis;

    protected override void Start()
    {
        base.Start();
        markedEnemis = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemis.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

            markedEnemis.Add(col.gameObject); //Marca al enemigo para evitar daño repetido
        }
    }
}
