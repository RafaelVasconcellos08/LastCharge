
using UnityEngine;

public class BateriaColetavel : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ColetarBateria();

            Destroy(gameObject);
        }
    }
}
