using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    private bool collected = false;

    public void Collect(PlayerInventory inventory)
    {
        if (collected) return;
        collected = true;
        inventory.AddKey();
        Destroy(gameObject);
    }
}