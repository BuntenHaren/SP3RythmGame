using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BossState
{
    protected BossBehaviour behaviour;
    protected BossStats stats;
    protected Health health;
    
    public virtual void Entry(BossBehaviour bossBehaviour, BossStats bossStats, Health bossHealth)
    {
        behaviour = bossBehaviour;
        stats = bossStats;
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
