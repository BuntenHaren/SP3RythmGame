using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhaseState : BossState
{
    public override void Entry(BossBehaviour bossBehaviour, BossStats firstPhase, BossStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        health.onChange += CheckForEnrage;
    }

    private void CheckForEnrage(float amount)
    {
        if(health.CurrentHealth <= firstPhaseStats.ThresholdForEnrage)
            behaviour.Transition(new NextPhaseState());
    }

    public override void Exit()
    {
        base.Exit();
        health.onChange -= CheckForEnrage;
    }
}
