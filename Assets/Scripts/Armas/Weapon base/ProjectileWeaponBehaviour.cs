using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Stats actuales
    protected float currentSpeed;
    protected float currentDamage;
    protected float currentCooldownDuration;
    protected float currentPierce;

    void Awake()
    {
        currentSpeed = weaponData.Speed;
        currentDamage = weaponData.Damage;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) //izquierda
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) //abajo
        {
            rotation.z = -180f;
        }
        else if (dirx == 0 && diry > 0) //arriba
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry > 0) //derecha arriba
        {
            rotation.z = -45f;
        }
        else if (dirx > 0 && diry < 0) //derecha abajo
        {
            rotation.z = -135f;
        }
        else if (dirx < 0 && diry > 0) //izquierda arriba
        {
            rotation.z = 45f;
            scale.x = scale.x * -1;
        }
        else if (dirx < 0 && diry < 0) //izquierda abajo
        {
            rotation.z = 135f;
            scale.x = scale.x * -1;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            reducePierce(); 
        }
    }

    void reducePierce()  //Destruye el proyectil si se ha alcanzado el limite de penetracion
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
