using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int Keys { get; private set; }

    public void AddKey()
    {
        Keys++;
        Debug.Log($"Số chìa: {Keys}");
    }
}