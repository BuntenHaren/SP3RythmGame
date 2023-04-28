using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BossState
{
    protected BossBehaviour behaviour;
    protected BossStats firstPhaseStats;
    protected BossStats secondPhaseStats;
    protected Health health;
    protected Timer timer;
    
    public virtual void Entry(BossBehaviour bossBehaviour, BossStats firstPhase, BossStats secondPhase, Health bossHealth)
    {
        timer = new Timer();
        timer.TimerDone += TimerDone;
        behaviour = bossBehaviour;
        firstPhaseStats = firstPhase;
        secondPhaseStats = secondPhase;
        health = bossHealth;
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        timer.UpdateTimer(Time.fixedDeltaTime);
    }

    protected virtual void TimerDone()
    {
        
    }

    public virtual void OnBeat()
    {
        
    }

    public virtual void Exit()
    {
        timer.TimerDone -= TimerDone;
    }
    
}
