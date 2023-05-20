using System.Collections;
using System.Collections.Generic;
using Bosses;
using UnityEngine;

public class MinionSpawnState : SecondPhaseState
{
    private int currentSpawnPoint;
    
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        if(secondPhaseStats.spawnPoints.Count == 0)
            secondPhaseStats.spawnPoints.Add(behaviour.transform.position);
    }
    
    private void SpawnObject(GameObject obj)
    { 
        GameObject newObj = GameObject.Instantiate(obj, secondPhaseStats.spawnPoints[currentSpawnPoint], Quaternion.identity);
        currentSpawnPoint++;
        
        if(currentSpawnPoint == secondPhaseStats.spawnPoints.Count)
            currentSpawnPoint = 0;
    }

    private void SpawnWave()
    {
        for(int i = 0; i < secondPhaseStats.MinionAmount; i++)
        {
            SpawnObject(secondPhaseStats.MinionToSpawn);
        }
    }
    
}
