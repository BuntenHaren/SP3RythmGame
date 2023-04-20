using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngager : MonoBehaviour
{
    [SerializeField]
    private EnemyBehavior[] enemies;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].engaged = true;
                if (!enemies[i].isActiveAndEnabled)
                {
                    enemies[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
