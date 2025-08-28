using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorAnimator : MonoBehaviour
{
    // Referencias

    Animator am;
    Movimiento pm;
    SpriteRenderer sr;
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<Movimiento>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.movimientoDir.x != 0 || pm.movimientoDir.y != 0)
        {
            am.SetBool("Mover", true);

            CheckDireccionSprite();
        }
        else
        {
            am.SetBool("Mover", false);
        }
    }

    void CheckDireccionSprite()
    {
        if (pm.ultimoMovimientoHorizontal < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
