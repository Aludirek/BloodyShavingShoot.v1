using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    private int currentHealth;

    [SerializeField] private int minHealth = 0, maxHealth = 100;
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Image healthFull;
    [SerializeField] private Text healthText;

    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explSfx;
    private AudioSource _adSrc;
    public void IncreaseHealth(int amount = 1)
    {
        if (currentHealth < maxHealth)
            currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    public void DecreaseHealth(int amount = 1)
    {
        if (currentHealth > minHealth)
            currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);

        if (currentHealth <= minHealth)
            Kill();
    }
    private void Explode()
    {
        // Animacja wybuchu gracza
        if (explosionEffect == null)
            return;
        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        if(explosionEffect !=null)
        {
            _adSrc.PlayOneShot(explSfx);
            AudioSource.PlayClipAtPoint(explSfx, transform.position, 1f);
        }
    }
    private void Kill()
    {
        Explode();
        if(GetComponent<Enemy>())
        {
            Score.Instance.IncreaseScore(GetComponent<Enemy>().ScoreToIncrease);
            if(GetComponent<Enemy>().isBoss)
            {
                //Poziom ukończony

            }
            EnemySpawn.enemiesDefeated++;
        }else if(GetComponent<PlayerControler>())
        {
            GameObject.FindObjectOfType<GameUI>().GameOver(); //Kończy gre 
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    private float FillAmount()
    {
        return (float) ((float)currentHealth / (float)maxHealth);
    }
    private void Update()
    {
        if (healthText != null)
            healthText.text = currentHealth.ToString();
        if (healthFull != null)
            healthFull.fillAmount = FillAmount();

        if (currentHealth <= minHealth)
            Kill();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();
        _adSrc = GetComponent<AudioSource>();
        _adSrc.volume = 1f;
        _adSrc.loop = false;
        _adSrc.playOnAwake = false;
    }
}
