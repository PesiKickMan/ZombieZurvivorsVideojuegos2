using UnityEngine;

public class TriggerOpenDoor : MonoBehaviour
{
    private Animator puertaAnim;
    [SerializeField] GameObject puertaParaAbrir;

    void Start()
    {
        puertaAnim = puertaParaAbrir.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            puertaAnim.SetBool("Abrir", true);
            puertaParaAbrir.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
