using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    private int currentHealth;

    [SerializeField] private int minHealth = 0, maxHealth = 100;
    [SerializeField] private Image healthFull;
    [SerializeField] private Text healthText;

    public void IncreaseHealth(int amount = 1)
    {
        if (currentHealth < maxHealth)
            currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    public void DecreaseHealth(int amount = 1)
    {
        if (currentHealth <= minHealth)
            Kill();
        if (currentHealth > minHealth)
            currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private float FillAmount()
    {
        return (float) ((float)currentHealth / (float)maxHealth);
    }
    private void Update()
    {
        if (currentHealth <= minHealth)
            Kill();

        if (healthText != null)
            healthText.text = currentHealth.ToString();
        if (healthFull != null)
            healthFull.fillAmount = FillAmount();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
}
