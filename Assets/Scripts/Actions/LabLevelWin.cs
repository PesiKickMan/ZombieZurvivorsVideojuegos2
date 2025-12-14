using UnityEngine;

public class LabLevelWin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DesbloquearNivel.nivel2Desbloqueado = true;
            gameManager.TriggerWin();
        }
    }
}
