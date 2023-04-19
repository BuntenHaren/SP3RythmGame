using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemyDetector : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;

    void Start()
    {
        enemyBehavior = transform.parent.gameObject.GetComponent<EnemyBehavior>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("ClosestEnemyDetector"))
        {
            enemyBehavior.closestEnemy = col.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("ClosestEnemyDetector"))
        {
            enemyBehavior.closestEnemy = null;
        }
    }
}
