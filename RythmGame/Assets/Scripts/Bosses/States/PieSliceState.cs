using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieSliceState : FirstPhaseState
{
    private int numberOfBeatsWaited;
    private bool attackTelegraphStarted;
    private bool startedAttacking;
    private Vector3 attackPosition;
    private GenerateCircle outerRingTelegraph;
    private GenerateCircle innerCircleTelegraph;

    public override void Entry(BossBehaviour bossBehaviour, BossStats firstPhase, BossStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        
    }

    public override void OnBeat()
    {
        if(!attackTelegraphStarted)
        {
            StartTelegraphAttack();
            return;
        }

        if(startedAttacking)
            return;
        
        numberOfBeatsWaited++;
        
        if(numberOfBeatsWaited >= firstPhaseStats.NumberOfBeatsWarningForStomp)
            StartAttack();
    }

    private void StartTelegraphAttack()
    {
        attackTelegraphStarted = true;
        attackPosition = behaviour.GetPlayerPos();
        
    }

    public override void Update()
    {
        
    }

    private void StartAttack()
    {
        startedAttacking = true;
        timer.StartTimer(3);
        
        

    }

    protected override void TimerDone()
    {
        behaviour.Transition(new IdleFirstPhase());
    }
}
