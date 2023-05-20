using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngager : MonoBehaviour
{
    [SerializeField]
    private EnemiesInCombatCounter enemiesInCombatCounter;

    [SerializeField]
    private EnemyBehavior[] enemies;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].engaged == false)
                {
                    enemies[i].engaged = true;
                    enemies[i].IdleSprite.SetActive(false);
                    enemies[i].SpritesParent.SetActive(true);
                    enemiesInCombatCounter.AddEnemyToList(enemies[i]);
                }
            }
        }
    }
}
