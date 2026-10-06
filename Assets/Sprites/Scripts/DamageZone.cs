using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
        }
    }
}