using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    protected int damage = 25;
    [SerializeField]
    protected float timeToDestroy = 3f; // Czas po jakim pocisk zniknie jeżeli na nic nie trafi
    [SerializeField]
    protected bool destroyProjectile = true;
    private bool collided = false;

    private float timer = 0f;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        collided = true;
        if (coll.gameObject.GetComponent<HPSystem>())
            coll.gameObject.GetComponent<HPSystem>().DecreaseHealth(damage);
        Destroy(gameObject);
    }

    //Sprawdzaj status collided co klatkę
    void Update()
    {
        if (!destroyProjectile)
            return;
        timer += Time.deltaTime;
        if ((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            //Zniszcz
            Destroy(gameObject);
        }
    }
}
