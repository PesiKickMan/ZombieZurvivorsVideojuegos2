using UnityEngine;

public class LabLevelWin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager.TriggerWin();
        }
    }
}
