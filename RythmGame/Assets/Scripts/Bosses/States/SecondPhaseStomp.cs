using System.Collections;
using System.Collections.Generic;
using Bosses;
using Bosses.States;
using FMODUnity;
using UnityEngine;

public class SecondPhaseStomp : SecondPhaseState
{
    private int numberOfBeatsWaited;
    private bool attackTelegraphStarted;
    private bool startedAttacking;
    private Vector3 attackPosition;
    private GenerateCircle outerRingTelegraph;
    private GenerateCircle innerCircleTelegraph;
    private float currentShockwaveSize;
    private float currentShockwaveTime;
    private bool shockwave = false;

    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        outerRingTelegraph = behaviour.GenerateCircles[0];
        innerCircleTelegraph = behaviour.GenerateCircles[1];
        behaviour.ResetTelegraphPositions();
        Debug.Log("Second phase stomp");
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
        
        if(numberOfBeatsWaited == secondPhaseStats.NumberOfBeatsWarningForStomp - 1)
            behaviour.bossAnim.SetTrigger("HoofStomp");
        
        if(numberOfBeatsWaited >= secondPhaseStats.NumberOfBeatsWarningForStomp)
            StartAttack();
    }

    private void StartTelegraphAttack()
    {
        attackTelegraphStarted = true;
        attackPosition = behaviour.GetPlayerPos();
        attackPosition.y = 0.05f;
        outerRingTelegraph.transform.position = attackPosition;
        outerRingTelegraph.SetMesh(outerRingTelegraph.CreateHollowCircle(100, secondPhaseStats.StompRadius - 0.1f, secondPhaseStats.StompRadius, 360, 0));
        innerCircleTelegraph.transform.position = attackPosition;
    }

    public override void Update()
    {
        innerCircleTelegraph.SetMesh(innerCircleTelegraph.CreateCircleMesh(100, secondPhaseStats.StompRadius * (numberOfBeatsWaited / (float)secondPhaseStats.NumberOfBeatsWarningForStomp), 360, 0));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(shockwave)
            UpdateShockwave();
    }

    private void StartAttack()
    {
        startedAttacking = true;
        timer.StartTimer(secondPhaseStats.StompShockwaveTime);
        outerRingTelegraph.SetMesh(new Mesh());
        
        RuntimeManager.PlayOneShot(secondPhaseStats.HoofStompSFX);
        
        HitEverythingInRadius();
        
        ActivateShockwave();
    }

    private void ActivateShockwave()
    {
        RuntimeManager.PlayOneShot(secondPhaseStats.StompShockwaveSFX);
        shockwave = true;
        innerCircleTelegraph.SetMesh(new Mesh());
    }

    private void UpdateShockwave()
    {
        currentShockwaveTime += Time.deltaTime;
        currentShockwaveSize = secondPhaseStats.StompShockwaveFinalSize * (currentShockwaveTime / secondPhaseStats.StompShockwaveTime);
        Mesh shockwave = outerRingTelegraph.CreateHollowCircle(100, currentShockwaveSize, currentShockwaveSize + secondPhaseStats.StompShockwaveWidth, 360, 0);
        outerRingTelegraph.SetMesh(shockwave);
    }

    private void HitEverythingInRadius()
    {
        Collider[] potentialHit = Physics.OverlapSphere(attackPosition, secondPhaseStats.StompRadius);
        foreach(Collider hit in potentialHit)
        {
            if(hit.gameObject.TryGetComponent<IDamageable>(out IDamageable damaged))
            {
                damaged.TakeDamage(secondPhaseStats.StompDamage);
            }
        }
    }

    protected override void TimerDone()
    {
        behaviour.Transition(new IdleSecondPhase());
    }

    public override void Exit()
    {
        base.Exit();
        innerCircleTelegraph.SetMesh(new Mesh());
        outerRingTelegraph.SetMesh(new Mesh());
    }
}
