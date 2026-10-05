using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int keysNeeded = 3;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerInventory inv = collision.gameObject.GetComponent<PlayerInventory>();
        if (inv == null) return;

        if (inv.Keys >= keysNeeded)
        {
            Debug.Log("Cửa mở!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Cần {keysNeeded} chìa, bạn mới có {inv.Keys}");
        }
    }
}