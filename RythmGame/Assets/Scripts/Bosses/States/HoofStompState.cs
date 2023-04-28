using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoofStompState : FirstPhaseState
{
    private int numberOfBeatsWaited = 1;
    private bool attackTelegraphStarted;
    private bool startedAttacking;
    private Vector3 attackPosition;
    private GenerateCircle outerRingTelegraph;
    private GenerateCircle innerCircleTelegraph;

    public override void Entry(BossBehaviour bossBehaviour, BossStats firstPhase, BossStats secondPhase, Health bossHealth)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth);
        outerRingTelegraph = behaviour.GenerateCircles[0];
        innerCircleTelegraph = behaviour.GenerateCircles[1];
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

        if(numberOfBeatsWaited + 1>= firstPhaseStats.NumberOfBeatsWarningForStomp)
            StartAttack();
    }

    private void StartTelegraphAttack()
    {
        attackTelegraphStarted = true;
        attackPosition = behaviour.GetPlayerPos();
        outerRingTelegraph.transform.position = attackPosition;
        outerRingTelegraph.SetMesh(outerRingTelegraph.CreateHollowCircle(100, firstPhaseStats.StompRadius - 0.1f, firstPhaseStats.StompRadius, 360));
        innerCircleTelegraph.transform.position = attackPosition;
    }

    public override void Update()
    {
        innerCircleTelegraph.SetMesh(innerCircleTelegraph.CreateCircleMesh(100, firstPhaseStats.StompRadius * (1 - (1 / (numberOfBeatsWaited))), 360));
    }

    private void StartAttack()
    {
        startedAttacking = true;
        timer.StartTimer(3);

        Collider[] potentialHit = Physics.OverlapSphere(attackPosition, firstPhaseStats.StompRadius);
        foreach(Collider hit in potentialHit)
        {
            if(hit.gameObject.TryGetComponent<IDamageable>(out IDamageable damaged))
            {
                damaged.TakeDamage(firstPhaseStats.StompDamage);
            }
        }

    }

    protected override void TimerDone()
    {
        behaviour.Transition(new IdleFirstPhase());
    }
}
