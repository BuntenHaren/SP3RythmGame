using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnPort spawnPort;
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private float timeBetweenSpawns;
    [SerializeField]
    private int totalAmountToSpawn;
    [SerializeField]
    private List<EnemyBehavior> enemiesToKillBefore;
    [SerializeField]
    private List<Transform> spawnPoints;

    private int spawnsLeft;
    private int currentSpawnPoint;
    private Timer spawnTimer;

    private void Start()
    {
        spawnsLeft = totalAmountToSpawn;
        spawnTimer = new Timer();
        spawnTimer.TimerDone += SpawnObject;
        if(spawnPoints.Count == 0)
            spawnPoints.Add(transform);
    }

    private void SpawnObject()
    {
        if(spawnsLeft <= 0)
            return;

        GameObject obj = Instantiate(objectToSpawn, spawnPoints[currentSpawnPoint]);
        currentSpawnPoint++;
        if(currentSpawnPoint == spawnPoints.Count)
            currentSpawnPoint = 0;
        spawnTimer.StartTimer(timeBetweenSpawns);
        spawnsLeft--;
        spawnPort.onEnemySpawn.Invoke(obj);
    }

    private void Update()
    {
        spawnTimer.UpdateTimer(Time.deltaTime);
        bool startSpawn = true;
        for(int i = 0; i < enemiesToKillBefore.Count; i++)
        {
            if(enemiesToKillBefore[i].isDead)
                startSpawn = false;
        }
        if(startSpawn)
            spawnTimer.StartTimer(timeBetweenSpawns);
    }
}
