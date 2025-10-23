using UnityEngine;

public class BulletBehaviour : ProjectileWeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.fire);
        }
    }

    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
