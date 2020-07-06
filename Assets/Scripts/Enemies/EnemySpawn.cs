using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int maxEnemies = 3; //Maksymalna ilosć przeciwników w tym samym czasie, jedna fala jeden przeciwnik
    [SerializeField] private GameObject[] enemiesToSpawn = new GameObject[2];

    private int interval = 20;

    private int GetNumberOfEnemies()
    {
        int no = 0;
        Enemy[] temp = GameObject.FindObjectsOfType<Enemy>();
        for (int i =0; i < temp.Length; ++i)
        {
            if (temp[i].enemyType == Enemy.EnemyType.Solo)
                no++;
        }
        no += GameObject.FindObjectsOfType<EnemyWave>().Length;
        return no;
    }

    private void SpawnEnemies()
    {
        if (GetNumberOfEnemies() >= maxEnemies)
            return;
        int n = Random.Range(0, enemiesToSpawn.Length);
        int i = GetNumberOfEnemies();
        for(; i <= maxEnemies; ++i)
        {
            n = Random.Range(0, enemiesToSpawn.Length);
            if(enemiesToSpawn[n] != null)
                Instantiate(enemiesToSpawn[n]);
        }
    }
   
    private void Start()
    {
        SpawnEnemies();
    }

    
    private void Update()
    {
        if(Time.frameCount % interval == 0)
            SpawnEnemies();
    }
}
