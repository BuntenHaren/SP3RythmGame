using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhaseState : BossState
{
    public override void Entry(BossBehaviour bossBehaviour, BossStats bossStats, Health bossHealth)
    {
        base.Entry(bossBehaviour, bossStats, bossHealth);
        health.onChange += CheckForEnrage;
    }

    private void CheckForEnrage(float amount)
    {
        if(health.CurrentHealth <= stats.ThresholdForEnrage)
            behaviour.Transition(new NextPhaseState());
    }
}
