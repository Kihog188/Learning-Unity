using UnityEngine;

public class ExitZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) gameManager.Win();
    }
}