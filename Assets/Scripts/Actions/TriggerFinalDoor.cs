using UnityEngine;

public class TriggerFinalDoor : MonoBehaviour
{
    [SerializeField] GameObject puertaParaAbrir;
    [SerializeField] GameObject boton1;
    [SerializeField] GameObject boton2;
    [SerializeField] GameObject boton3;

    private Animator puertaAnim;
    private bool boton1Tocado = false;
    private bool boton2Tocado = false;
    private bool boton3Tocado = false;

    void Start()
    {
        if (puertaParaAbrir == null)
        {
            Debug.LogError("TriggerFinalDoor: 'puertaParaAbrir' no está asignado en el Inspector.", this);
            return;
        }

        puertaAnim = puertaParaAbrir.GetComponent<Animator>();
        
        if (puertaAnim == null)
        {
            Debug.LogError("TriggerFinalDoor: no se encontró Animator en 'puertaParaAbrir'.", puertaParaAbrir);
        }

        VerificarBotones();

    }

    void VerificarBotones()
    {
        if (boton1 == null || boton2 == null || boton3 == null)
        {
            Debug.LogError("TriggerFinalDoor: no todos los botones están asignados en el Inspector.", this);
        }
    }

    /// <summary>
    /// Llamado por los scripts de botones cuando el jugador los toca
    /// </summary>
    public void ReportarBotonTocado(int numeroBoton)
    {
        if (numeroBoton == 1)
        {
            boton1Tocado = true;
            Debug.Log("Botón 1 tocado");
        }
        else if (numeroBoton == 2)
        {
            boton2Tocado = true;
            Debug.Log("Botón 2 tocado");
        }
        else if (numeroBoton == 3)
        {
            boton3Tocado = true;
            Debug.Log("Botón 3 tocado");
        }

        VerificarYAbrirPuerta();
    }

    void VerificarYAbrirPuerta()
    {
        if (boton1Tocado && boton2Tocado && boton3Tocado)
        {
            if(SoundManager.instance != null)
                SoundManager.instance.PlaySFX(SoundManager.instance.doorOpen);
            
            AbrirPuerta();
        }
    }

    void AbrirPuerta()
    {
        if (puertaAnim != null)
        {
            puertaAnim.SetBool("Abrir", true);
            Debug.Log("¡Puerta abierta! Los 3 botones fueron tocados.");

            // Desactivar el colisionador de la puerta para que no bloquee
            BoxCollider2D collider = puertaParaAbrir.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
                Debug.Log("BoxCollider2D desactivado en: " + puertaParaAbrir.name);
            }
            else
            {
                Debug.LogWarning("TriggerFinalDoor: no se encontró BoxCollider2D en 'puertaParaAbrir' (" + puertaParaAbrir.name + ")", puertaParaAbrir);
            }
        }
    }
}
