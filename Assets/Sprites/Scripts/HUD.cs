using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text keysText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int totalKeys = 3;
    
    private float elapsed;
    public float Elapsed => elapsed;

    void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealth;
        inventory.OnKeysChanged += UpdateKeys;
    }

    void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
        inventory.OnKeysChanged -= UpdateKeys;
    }

    void Start()
    {
        UpdateHealth(playerHealth.Current, playerHealth.MaxHealth);
        UpdateKeys(inventory.Keys);
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        int minutes = (int)(elapsed / 60);
        int seconds = (int)(elapsed % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void UpdateHealth(int current, int max)
    {
        healthText.text = $"Health: {current}/{max}";
    }

    void UpdateKeys(int keys)
    {
        keysText.text = $"Keys: {keys}/{totalKeys}";
    }
}