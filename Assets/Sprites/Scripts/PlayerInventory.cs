using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int Keys { get; private set; }

    public void AddKey()
    {
        Keys++;
        Debug.Log($"Số chìa: {Keys}");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable item))
        {
            item.Collect(this);
        }
    }
}