using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    // Movimiento
    [HideInInspector]
    public float ultimoMovimientoHorizontal;
    [HideInInspector]
    public float ultimoMovimientoVertical;
    [HideInInspector]
    public Vector2 movimientoDir;
    [HideInInspector]
    public Vector2 vectorUltimoMovimiento;

    //Referencias
    Rigidbody2D rb;
    public CharacterScriptableObject characterData;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vectorUltimoMovimiento = new Vector2(1, 0f);
    }

    void Update()
    {
        InputManagment();
    }

    void FixedUpdate()
    {
        Mover();
    }

    void InputManagment()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movimientoDir = new Vector2(horizontal, vertical).normalized;

        if (movimientoDir.x != 0)
        {
            ultimoMovimientoHorizontal = movimientoDir.x;
            vectorUltimoMovimiento = new Vector2(ultimoMovimientoHorizontal, 0f);
        }

        if (movimientoDir.y != 0)
        {
            ultimoMovimientoVertical = movimientoDir.y;
            vectorUltimoMovimiento = new Vector2(0f, ultimoMovimientoVertical);
        }

        if (movimientoDir.x != 0 && movimientoDir.y != 0)
        {
            vectorUltimoMovimiento = new Vector2(ultimoMovimientoHorizontal, ultimoMovimientoVertical);
        }
    }

    void Mover()
    {
        rb.linearVelocity = new Vector2(movimientoDir.x * characterData.MoveSpeed, movimientoDir.y * characterData.MoveSpeed);
    }
}
