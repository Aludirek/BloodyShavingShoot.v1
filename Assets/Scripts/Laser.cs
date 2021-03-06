﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 3f; // Czas po jakim laser zniknie jeżeli na nic nie trafi
    private bool collided = false;
    [SerializeField]
    private int damage = 25;

    private float timer = 0f;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        collided = true;
        if (coll.gameObject.GetComponent<HPSystem>())
            coll.gameObject.GetComponent<HPSystem>().DecreaseHealth(damage);
        PlayerControler.Instance.ReleaseLaser(gameObject);
    }

    //Sprawdzaj status collided co klatkę
    void Update()
    {
        timer += Time.deltaTime;
        if ((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            //Zniszcz
            PlayerControler.Instance.ReleaseLaser(gameObject);
        }
    }
}
