using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToEnemiesInCombat : MonoBehaviour
{
    [SerializeField]
    private EnemyBehavior enemyBehavior;
    [SerializeField]
    private EnemiesInCombatCounter enemiesInCombatCounter;

    void Start()
    {
        enemiesInCombatCounter.AddEnemyToList(enemyBehavior);
    }
}
