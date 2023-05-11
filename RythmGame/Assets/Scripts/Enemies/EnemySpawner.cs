using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] 
    private SpawnPort spawnPort;
    [SerializeField]
    private DeathPort deathPort;
    [SerializeField] 
    private List<Wave> waves;
    [SerializeField]
    private List<Transform> spawnPoints;

    private int currentSpawnPoint;
    private int currentWave;
    private List<GameObject> EnemiesAlive;

    private void OnEnable()
    {
        deathPort.onEnemyDeath += CountDeaths;
    }

    private void OnDisable()
    {
        deathPort.onEnemyDeath -= CountDeaths;
    }

    private void Awake()
    {
        if(spawnPoints.Count == 0)
            spawnPoints.Add(transform);
    }

    private void SpawnObject(GameObject obj)
    { 
        GameObject newObj = Instantiate(obj, spawnPoints[currentSpawnPoint]);
        currentSpawnPoint++;
        
        if(currentSpawnPoint == spawnPoints.Count)
            currentSpawnPoint = 0;
        
        spawnPort.onEnemySpawn.Invoke(newObj);
        EnemiesAlive.Add(newObj);
    }

    private void SpawnWave()
    {
        if(currentWave >= waves.Count)
            return;
        
        for(int i = 0; i < waves[currentWave].objects.Count; i++)
        {
            SpawnObject(waves[currentWave].objects[i]);
        }

        currentWave++;
    }

    private void CountDeaths(GameObject death)
    {
        if(EnemiesAlive.Contains(death))
        {
            EnemiesAlive.Remove(death);
        }

        EnemiesAlive.RemoveAll(obj => obj == null);
        
        if(EnemiesAlive.Count == 0)
            SpawnWave();
    }
    
}

[CreateAssetMenu(menuName = "RythmGame/Enemy/Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> objects;
}
