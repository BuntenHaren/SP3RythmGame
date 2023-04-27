using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AdaptiveMusicManager : MonoBehaviour
{
    [SerializeField]
    private StudioGlobalParameterTrigger parameterTrigger;
    [SerializeField]
    private SpawnPort spawnPort;
    [SerializeField]
    private DeathPort deathPort;

    private int enemiesCurrentlyAlive;

    private void Start()
    {
        if(parameterTrigger == null)
            parameterTrigger = GetComponent<StudioGlobalParameterTrigger>();
    }

    private void UpdateMusicValue()
    {
        parameterTrigger.Value = enemiesCurrentlyAlive;
        parameterTrigger.TriggerParameters();
    }
    
    private void IncreaseEnemyCount(GameObject obj)
    {
        enemiesCurrentlyAlive++;
        UpdateMusicValue();
    }
    
    private void DecreaseEnemyCount(GameObject obj)
    {
        enemiesCurrentlyAlive--;
        UpdateMusicValue();
    }
    
    private void OnEnable()
    {
        spawnPort.onEnemySpawn += IncreaseEnemyCount;
        deathPort.onEnemyDeath += DecreaseEnemyCount;
    }

    private void OnDisable()
    {
        spawnPort.onEnemySpawn -= IncreaseEnemyCount;
        deathPort.onEnemyDeath -= DecreaseEnemyCount;
    }
}
