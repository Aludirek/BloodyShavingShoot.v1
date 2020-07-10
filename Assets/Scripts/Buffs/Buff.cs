using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType
    {
        Ammo, Health
    }

    [SerializeField] private BuffType buffType;
    [SerializeField] private int perkToGive = 20;

    private void Start()
    {
        transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 0f));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(buffType == BuffType.Ammo)
            {
                //Zwiększa amunicje
                if (StatsManager.Instance != null)
                    StatsManager.Instance.AddLaserByAmount(perkToGive);
            }else
            {
                //Zwiększa Hp
                if (col.gameObject.GetComponent<HPSystem>())
                    col.gameObject.GetComponent<HPSystem>().IncreaseHealth(perkToGive);
            }
            Destroy(gameObject);
        }
    }
}
