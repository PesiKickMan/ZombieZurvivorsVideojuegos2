using UnityEngine;

public class BloodPoolController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBloodPool = Instantiate(weaponData.Prefab);
        spawnedBloodPool.transform.position = transform.position;
        spawnedBloodPool.transform.parent = transform;
    }
}
