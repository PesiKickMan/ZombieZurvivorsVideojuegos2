using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;

    // Guardar último movimiento
    [HideInInspector]
    public Vector2 ultimoMovimiento;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    [System.Obsolete]
    void Start()
    {
        player = FindFirstObjectByType<Movimiento>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        ultimoMovimiento = Vector2.right; 
    }

    void Update()
    {
        //Me fijo la direccion hacia el jugador y guardo el ultimo movimiento
        Vector2 direccion = (player.position - transform.position).normalized;

        if (direccion.magnitude > 0.01f)
        {
            ultimoMovimiento = direccion;
        }

        //Movimiento del enemigo
        rb.MovePosition(Vector2.MoveTowards(rb.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime));

        if (ultimoMovimiento.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (ultimoMovimiento.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
