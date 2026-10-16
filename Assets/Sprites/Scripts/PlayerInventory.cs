using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int Keys { get; private set; }
    public event Action<int> OnKeysChanged;

    public void AddKey()
    {
        Keys++;
        OnKeysChanged?.Invoke(Keys);
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable item))
        {
            item.Collect(this);
        }
    }
}