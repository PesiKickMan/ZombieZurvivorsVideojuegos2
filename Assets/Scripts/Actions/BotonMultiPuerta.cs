using UnityEngine;

public class BotonMultiPuerta : MonoBehaviour
{
    [SerializeField] int numeroBoton = 1; // 1, 2 o 3
    [SerializeField] TriggerFinalDoor puertaControlada;
    private bool yaFueTocado = false;


    void Start()
    {
        if (puertaControlada == null)
        {
            Debug.LogError("BotonMultiPuerta: 'puertaControlada' no está asignada en el Inspector en GameObject: " + gameObject.name, this);
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !yaFueTocado)
        {
            yaFueTocado = true;

            // Notificar a la puerta que este botón fue tocado
            puertaControlada.ReportarBotonTocado(numeroBoton);

            Debug.Log("Botón " + numeroBoton + " fue tocado en: " + gameObject.name);
        }
    }
}
