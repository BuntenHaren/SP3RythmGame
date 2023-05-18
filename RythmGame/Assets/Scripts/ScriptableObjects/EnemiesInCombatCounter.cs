using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Counters/EnemiesInCombatCounter")]
public class EnemiesInCombatCounter : ScriptableObject
{
    public List<EnemyBehavior> engagedEnemies = new List<EnemyBehavior>();
    public bool inCombat = false;

    void OnDisable()
    {
        engagedEnemies.Clear();
        inCombat = false;
    }

    public void AddEnemyToList(EnemyBehavior enemy)
    {
        engagedEnemies.Add(enemy);
        if(engagedEnemies.Count > 0)
        {
            inCombat = true;
        }
    }

    public void RemoveEnemyFromList(EnemyBehavior enemy)
    {
        engagedEnemies.Remove(enemy);
        if (engagedEnemies.Count == 0)
        {
            inCombat = false;
        }
    }
}
