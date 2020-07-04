using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileToSpawn; // Pociski dla przeciwników
    [SerializeField]
    private float fireRate = 0.3f;
    [SerializeField]
    private Vector2 projectileSpawnOffset = new Vector2(0f, -1.3f);
    [SerializeField]
    private Vector2 projectilieSpeed = new Vector2(0f, -1f);

    private void Fire()
    {
        Vector2 targetPos = new Vector2(transform.position.x + projectileSpawnOffset.x, transform.position.y + projectileSpawnOffset.y);
        GameObject proj = Instantiate(projectileToSpawn, targetPos, projectileToSpawn.transform.rotation, transform) as GameObject;
        if (proj.GetComponent<Rigidbody2D>())
            proj.GetComponent<Rigidbody2D>().AddForce(projectilieSpeed, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (projectileToSpawn != null)
            InvokeRepeating("Fire", 0.01f, fireRate);
    }
}
