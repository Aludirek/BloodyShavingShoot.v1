using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private GameObject projectileToSpawn; // Pociski dla przeciwników
    [SerializeField]private float fireRate = 0.3f;
    [SerializeField]private Vector2 projectileSpawnOffset = new Vector2(0f, -1.3f);
    [SerializeField]private Vector2 projectilieSpeed = new Vector2(0f, -1f);
    [SerializeField] private bool moveTo = true;
    public bool isBoss = false;
    [SerializeField] private GameObject haveHP;

    public enum EnemyType
    {
        Solo, Wave
    }

    [SerializeField]public EnemyType enemyType;

    private float minHeight = 0f, maxHeight = 0f, minWidth = 0f, maxWidth = 0f;
    private Vector2 vel1, vel2;
    [SerializeField][Range(1f, 10f)]private float smoothTime = 1f;

    private Vector2 minW, maxW;
    private float maxSpeed = 10f;


    private void Fire()
    {
        Vector2 targetPos = new Vector2(transform.position.x + projectileSpawnOffset.x, transform.position.y + projectileSpawnOffset.y);
        GameObject proj = Instantiate(projectileToSpawn, targetPos, projectileToSpawn.transform.rotation, transform) as GameObject;
        if (proj.GetComponent<Rigidbody2D>())
            proj.GetComponent<Rigidbody2D>().AddForce(projectilieSpeed, ForceMode2D.Impulse);
    }

    private bool closerToMin = false, closerToMax = false, exec = false;
    
    private void Update()
    {
        if (enemyType == EnemyType.Solo && moveTo)
        {
            if (!exec)
            {
                transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                if (transform.position.x - 0.1f <= minW.x)
                {
                    closerToMin = true;
                    closerToMax = false;
                    exec = true;
                }
            }
            else
            {
                if (transform.position.x - 0.1f <= minW.x)
                {
                    closerToMin = true;
                    closerToMax = false;
                }
                if (transform.position.x + 0.1f >= maxW.x)
                {
                    closerToMax = true;
                    closerToMin = false;
                }

                if (closerToMax)
                    transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                else if (closerToMin)
                    transform.position = Vector2.SmoothDamp(transform.position, maxW, ref vel2, smoothTime, maxSpeed, Time.deltaTime);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        smoothTime = Random.Range(0, 1);
        if (projectileToSpawn != null)
            InvokeRepeating("Fire", 0.1f, fireRate);
        if(haveHP != null)
        {
            haveHP.SetActive(true);
        }
        if(enemyType == EnemyType.Solo)
        {
            minHeight = Screen.height * 0.8f;
            maxHeight = Screen.height * 0.9f;
            Vector2 minH = Camera.main.ScreenToWorldPoint(new Vector2(0f, minHeight));
            Vector2 maxH = Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight));
            float height = Random.Range(minH.y, maxH.y);

            minWidth = Screen.height * 0.2f;
            maxWidth = Screen.height * 2f;
            minW = Camera.main.ScreenToWorldPoint(new Vector2(minWidth, 0f));
            maxW = Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f));
            minW.y = height;
            maxW.y = height;
            float width = Random.Range(minW.x, maxW.x);

            transform.position = new Vector2(width, height);
        }
    }
}
