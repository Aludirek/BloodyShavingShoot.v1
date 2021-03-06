﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    [SerializeField]private bool infiniteCharge = false;
    [SerializeField]
    private int noOfLasers = 200, noOfMissile = 25;
    [SerializeField]
    private Text laserText, missileText;

    public bool CheckIfCanShootL(int amount)
    {
        if (infiniteCharge)
            return true;

        bool p = false;
        if (noOfLasers - amount >= 0)
            p = true;

        return p;
    }

    public bool CheckIfCanShootM(int amount)
    {
        bool p = false;
        if (noOfMissile - amount >= 0)
            p = true;

        return p;
    }

    public void ShootLaserByAmount(int amount)
    {
        if (infiniteCharge)
            return;

        if (noOfLasers - amount >= 0)
            noOfLasers -= amount;
    }

    public void AddLaserByAmount(int amount)
    {
        if (infiniteCharge)
            return;

        if (amount >= 0)
            noOfLasers += amount;
    }
    public void ShootMissileByAmount(int amount)
    {
        if (noOfMissile - amount >= 0)
            noOfMissile -= amount;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;
        if(laserText != null)
        {
            laserText.text = ("Lasers:" + noOfLasers).ToString();
        }

        if(missileText != null)
        {
            missileText.text = ("Missile:" + noOfMissile).ToString();
        }
    }
}
