using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PlayerControler:MonoBehaviour
{
    public static PlayerControler Instance;
    public enum MovementInputType
    {
        ButtonBased
    }

    [SerializeField]
    private MovementInputType movementInputType;
    [SerializeField]
    private KeyCode left = KeyCode.LeftArrow, right = KeyCode.RightArrow;

    [SerializeField]
    private VButton lvB, rvB;

    [SerializeField]
    private float Speed = 10f;
    [SerializeField]
    private Vector2 minPos, maxPos;

    private Vector2 pos;

    [Header("Laser")]
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Vector2 laserSpeed = new Vector2 (0f, 1f);
    [SerializeField]
    private Vector3 spawnOffset;
    [SerializeField]
    private float laserFireRate = 0.2f;
    [SerializeField]
    private KeyCode laserKey = KeyCode.Mouse1;

    private ObjectPool laserPool;
    [SerializeField]
    private int laserPoolSize = 30;

    [Header("Missile")]
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private Vector2 missileSpeed = new Vector2(0f, 1f);
    [SerializeField]
    private Vector3 spawnOffsetmissile;
    [SerializeField]
    private float missileFireRate = 0.2f;
    [SerializeField]
    private KeyCode missileKey = KeyCode.Mouse1;

    private ObjectPool missilePool;
    [SerializeField]
    private int missilePoolSize = 30;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        laserPool = new ObjectPool(laser, laserPoolSize);
        missilePool = new ObjectPool(missile, missilePoolSize);
    }

    public void ReleaseLaser(GameObject laser)
    {
        laserPool.ReturnInstance(laser);
    }

    public void ReleaseMissile(GameObject missile)
    {
        missilePool.ReturnInstance(missile);
    }

    private void Fire()
    {
        GameObject laserInstance = laserPool.GetInstance();
        laserInstance.transform.position = transform.position + spawnOffset;
        laserInstance.GetComponent<Rigidbody2D>().AddForce(laserSpeed, ForceMode2D.Impulse);
    }

    private void MissileFire()
    {
        GameObject missileInstance = missilePool.GetInstance();
        missileInstance.transform.position = transform.position + spawnOffsetmissile;
        missileInstance.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        Instance = this;
        if(movementInputType == MovementInputType.ButtonBased)
        {
#if UNITY_STANDALONE
            //Test klawiszy
            /*
            if (Input.GetKey(left))
                transform.Translate(Speed * Vector2.left * Time.deltaTime);
            else if (Input.GetKey(right))
                transform.Translate(Speed * Vector2.right * Time.deltaTime);
            */
#endif
           
#if UNITY_ANDROID
            //Test przycisków
            if (lvB != null && rvB != null)
            {
                if (lvB.value)
                    transform.Translate(Speed * Vector2.left * Time.deltaTime);
                else if (rvB.value)
                    transform.Translate(Speed * Vector2.right * Time.deltaTime);
            }
#endif

        }

#if UNITY_STANDALONE
        if (Input.GetKeyDown(laserKey))
            InvokeRepeating("Fire", 0.021f, laserFireRate);

        if (Input.GetKeyUp(laserKey))
            CancelInvoke("Fire");

        if (Input.GetKeyDown(missileKey))
            MissileFire();
#endif

        pos.x = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(transform.position.y, minPos.y, maxPos.y);

        transform.position = pos;
    }

}
