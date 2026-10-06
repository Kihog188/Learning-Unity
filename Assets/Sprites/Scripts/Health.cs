using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invincibleTime = 1f;

    public int Current { get; private set; }

    private bool isInvincible = false;
    private SpriteRenderer sr;

    void Awake()
    {
        Current = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (Current <= 0 || isInvincible) return;

        Current = Mathf.Max(Current - amount, 0);
        Debug.Log($"Máu: {Current}/{maxHealth}");

        if (Current == 0)
        {
            Debug.Log("Thua!");
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        float timer = 0f;
        while (timer < invincibleTime)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            timer += 0.2f;
        }

        isInvincible = false;
    }
}