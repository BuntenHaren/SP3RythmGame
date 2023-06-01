using System.Collections;
using System.Collections.Generic;
using Bosses;
using Bosses.States;
using UnityEngine;

public class IdleSecondPhase : SecondPhaseState
{
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        timer.StartTimer(Random.Range(secondPhase.IdleTimeRangeMin, secondPhase.IdleTimeRangeMax));
        behaviour.bossAnim.Play("Boss_Idle_Phase_2");
        behaviour.ResetTelegraphPositions();
    }

    protected override void TimerDone()
    {
        float nextAttack = Random.Range(0f, secondPhaseStats.ChanceForStomp + secondPhaseStats.ChanceForCircle + secondPhaseStats.ChanceForMinionSpawn);
        if(nextAttack <= secondPhaseStats.ChanceForStomp)
            behaviour.Transition(new SecondPhaseStomp());
        else if(nextAttack <= secondPhaseStats.ChanceForStomp + secondPhaseStats.ChanceForCircle)
            behaviour.Transition(new SecondPhasePieSlice());
        else
            behaviour.Transition(new MinionSpawnState());
    }
}
