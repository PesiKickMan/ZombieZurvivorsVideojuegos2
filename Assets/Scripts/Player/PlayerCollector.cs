using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        //Me fijo si el objeto colisionado pertence a la interfaz ICollectible (Osea, si es un pick-up)
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Si es un pick-up, lo recolecto
            collectible.Collect();
        }
    }
}
