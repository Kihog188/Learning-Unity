using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;
    [SerializeField] private int contactDamage = 1;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (collision.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(contactDamage);
        }
    }
}