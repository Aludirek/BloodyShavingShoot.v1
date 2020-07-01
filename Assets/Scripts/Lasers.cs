using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 1f; // Czas po jakim rakieta wybuchnie jeżeli na nic nei trafi
    private bool collided = false;

    private float timer = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
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
