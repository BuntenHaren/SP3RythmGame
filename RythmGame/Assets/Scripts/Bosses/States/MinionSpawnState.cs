using System.Collections;
using System.Collections.Generic;
using Bosses;
using FMODUnity;
using UnityEngine;

public class MinionSpawnState : SecondPhaseState
{
    private int currentSpawnPoint;
    
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        if(secondPhaseStats.spawnPoints.Count == 0)
            secondPhaseStats.spawnPoints.Add(behaviour.transform.position);
        timer.StartTimer(2);
        behaviour.bossAnim.SetTrigger("Summon");
        RuntimeManager.PlayOneShot(secondPhaseStats.MinionSpawnSFX);
        SpawnWave();
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

    protected override void TimerDone()
    {
        base.TimerDone();
        behaviour.Transition(new IdleSecondPhase());
    }
}
