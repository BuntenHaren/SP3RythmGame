using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwipeCollision : MonoBehaviour
{
    private EnemyMeleeAttack swipeScript;

    void Start()
    {
        swipeScript = gameObject.GetComponentInParent<EnemyMeleeAttack>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            swipeScript.inSwipeTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            swipeScript.inSwipeTrigger = false;
        }
    }
}
