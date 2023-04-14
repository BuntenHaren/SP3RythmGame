using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField]
    private MeleeEnemy enemyParent;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            enemyParent.inAttackRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            enemyParent.inAttackRange = false;
        }
    }
}
