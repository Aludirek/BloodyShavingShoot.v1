using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int maxEnemies = 3; //Maksymalna ilosć przeciwników w tym samym czasie, jedna fala jeden przeciwnik
    [SerializeField] private GameObject[] enemiesToSpawn = new GameObject[2];

    [SerializeField] private int enemyDeathsToSpawnBoss = 4; //Ilość przeciwników zanim pojawi się boss
    [SerializeField] private GameObject hpBar;
    [SerializeField] private GameObject enemyBossToSpawn;

    public static int enemiesDefeated = 0;
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
        if (hpBar != null)
            hpBar.SetActive(true);
    }

    private void SpawnEnemyBoss()
    {
        if (enemyBossToSpawn != null)
            Instantiate(enemyBossToSpawn);
    }
    private bool exec = false;

    private void Update()
    {
        if(enemiesDefeated >= enemyDeathsToSpawnBoss)
        {
            if(!exec)
            {
                //Przywołaj bossa
                SpawnEnemyBoss();
                exec = true;
            }
        }else
        {
            if (Time.frameCount % interval == 0)
                SpawnEnemies();
        }
    }
}
