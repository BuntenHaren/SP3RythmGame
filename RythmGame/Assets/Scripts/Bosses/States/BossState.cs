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
    
    public virtual void Entry(BossBehaviour bossBehaviour, BossStats firstPhase, BossStats secondPhase, Health bossHealth)
    {
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
        
    }

    public virtual void OnBeat()
    {
        
    }

    public virtual void Exit()
    {
        
    }
    
}
