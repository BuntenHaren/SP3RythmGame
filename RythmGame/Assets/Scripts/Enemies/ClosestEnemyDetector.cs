using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemyDetector : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;
    private Transform parentTransform;

    void Start()
    {
        enemyBehavior = transform.parent.gameObject.GetComponent<EnemyBehavior>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("ClosestEnemyDetector"))
        {
            if(enemyBehavior != null || Vector3.Distance(parentTransform.position, col.transform.position) < Vector3.Distance(parentTransform.position, enemyBehavior.transform.position))
            {
                enemyBehavior.closestEnemy = col.gameObject.transform;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("ClosestEnemyDetector") && col.gameObject.transform == enemyBehavior.closestEnemy)
        {
            enemyBehavior.closestEnemy = null;
        }
    }
}
