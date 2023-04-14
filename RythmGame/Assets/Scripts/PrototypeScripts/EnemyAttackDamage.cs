using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDamage : MonoBehaviour
{
    [SerializeField]
    private int damage = 20;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().PlayerTakeDamage(damage);
        }
    }
}
