using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private float explosionEffectLength = 10f;

    [SerializeField]
    private float timeToDestroy = 10f; // Czas po jakim rakieta wybuchnie jeżeli na nic nie trafi
    private bool collided = false;
    [SerializeField]
    private int damage = 100;

    private float timer = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if(explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation) as GameObject;
            Destroy(explosion, explosionEffectLength);
        }
        if (collision.gameObject.GetComponent<HPSystem>())
            collision.gameObject.GetComponent<HPSystem>().DecreaseHealth(damage);
        PlayerControler.Instance.ReleaseMissile(gameObject);
    }

    //Sprawdzaj status collided co klatkę
    void Update()
    {
        timer += Time.deltaTime;
        if ((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            //Zniszcz
            PlayerControler.Instance.ReleaseMissile(gameObject);
        }
    }
}
