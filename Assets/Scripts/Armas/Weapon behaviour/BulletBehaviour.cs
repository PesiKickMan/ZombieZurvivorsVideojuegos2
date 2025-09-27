using UnityEngine;

public class BulletBehaviour : ProjectileWeaponBehaviour
{
    private SoundManager soundManager;

    protected override void Start()
    {
        base.Start();
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        soundManager.PlaySFX(soundManager.fire);
    }

    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
