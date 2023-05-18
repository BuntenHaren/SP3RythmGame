using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBlockage : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackOnBeatBall;
    [SerializeField]
    private List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    public void AddEnemyToList(EnemyBehavior enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(EnemyBehavior enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            OpenGate();
        }
    }

    public void OpenGate()
    {
        AttackOnBeatBall.SetActive(true);
    }
}
