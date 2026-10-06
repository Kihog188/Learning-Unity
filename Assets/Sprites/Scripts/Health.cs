using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    public int Current { get; private set; }

    void Awake()
    {
        Current = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (Current <= 0) return;

        Current = Mathf.Max(Current - amount, 0);
        Debug.Log($"Máu: {Current}/{maxHealth}");

        if (Current == 0)
        {
            Debug.Log("Thua!");
            gameObject.SetActive(false);
        }
    }
}