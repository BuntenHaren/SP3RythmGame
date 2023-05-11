using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBlockage : MonoBehaviour
{
    private List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    [SerializeField]
    private GameObject AttackOnBeatBall;

    public void AddEnemyToList(EnemyBehavior enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyBehavior enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count <= 0)
        {
            RoomCleared();
        }
    }

    public void RoomCleared()
    {
        AttackOnBeatBall.SetActive(true);
    }
}
