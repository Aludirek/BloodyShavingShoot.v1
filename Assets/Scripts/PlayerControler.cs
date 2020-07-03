using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PlayerControler : MonoBehaviour
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
    private Vector2 laserSpeed = new Vector2(0f, 3f);
    [SerializeField]
    private Vector3 spawnOffset;
    [SerializeField]
    private float laserFireRate = 0.3f;
    [SerializeField]
    private KeyCode laserKey = KeyCode.Mouse0;

    private ObjectPool laserPool;
    [SerializeField]
    private int laserPoolSize = 30;

    /*[Header("Laser2")]
    [SerializeField]
    private GameObject laser2;
    [SerializeField]
    private Vector2 laserSpeed2 = new Vector2(0f, 1f);
    [SerializeField]
    private Vector3 spawnOffset2;
    [SerializeField]
    private float laserFireRate2 = 0.2f;
    [SerializeField]
    private KeyCode laserKey2 = KeyCode.Mouse1;

    private ObjectPool laserPool2;
    [SerializeField]
    private int laserPoolSize2 = 30;
    */
    [Header("Missile")]
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private Vector2 missileSpeed = new Vector2(0f, 0.5f);
    [SerializeField]
    private Vector3 spawnOffsetMissile;
    [SerializeField]
    private KeyCode missileKey = KeyCode.Mouse3;

    private ObjectPool missilePool;
    [SerializeField]
    private int missilePoolSize = 30;

    [SerializeField]
    private VButton laserVB, missileVB;
    private bool laserPressed, missilePressed;

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
        if (StatsManager.Instance.CheckIfCanShootL(1))
        {
            GameObject laserInstance = laserPool.GetInstance();
            laserInstance.transform.position = transform.position + spawnOffset;
            laserInstance.GetComponent<Rigidbody2D>().AddForce(laserSpeed, ForceMode2D.Impulse);
            StatsManager.Instance.ShootLaserByAmount(1);
        }
    }

    private void MissileFire()
    {
        if (StatsManager.Instance.CheckIfCanShootM(1))
        {
            GameObject missileInstance = missilePool.GetInstance();
            missileInstance.transform.position = transform.position + spawnOffsetMissile;
            missileInstance.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
            StatsManager.Instance.ShootMissileByAmount(1);
        }
    }

    void GetInput()
    {
        if (laserVB != null)
            laserPressed = laserVB;
        if (missileVB != null)
            missilePressed = missileVB;
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;
        GetInput();
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
            InvokeRepeating("Fire", 0.02f, laserFireRate);

        if (Input.GetKeyUp(laserKey))
            CancelInvoke("Fire");

        if (Input.GetKeyDown(missileKey))
            MissileFire();
#endif
       
#if UNITY_ANDROID
        if (laserPressed && laserVB.value1)
        {
            InvokeRepeating("Fire", 0.02f, laserFireRate);
            laserVB.value1 = false;
        }

        if (laserPressed && !laserVB.value1)
        {
            CancelInvoke("Fire");
        }

        if (missilePressed && missileVB.value1)
        {
            MissileFire();
            missileVB.value1 = false;
        }
#endif

        pos.x = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(transform.position.y, minPos.y, maxPos.y);

        transform.position = pos;
    }

}
