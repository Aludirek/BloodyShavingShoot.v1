using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    private int currentHealth;

    [SerializeField]
    private int minHealth = 1, maxHealth = 100;

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
    private void Update()
    {
        if (currentHealth <= minHealth)
            Kill();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
}
