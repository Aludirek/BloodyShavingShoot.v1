using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] buffs = new GameObject[2];
    [SerializeField] private float randomGen = 0.0f;
    [SerializeField] private float cooldown = 6f;

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        int n = Random.Range(0, buffs.Length);
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            if (buffs [n] !=null)
            {
                if(Random.value <= randomGen)
                {
                    Instantiate(buffs[n].gameObject, transform);
                }
            }
            timer = 0f;

        }
        
    }
}
