using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField]
    private BossStats bossStats;
    [SerializeField]
    private Health bossHealth;
    
    private BossState currentBossState;

    private void Start()
    {
        currentBossState.Entry(this);
    }

    private void Update()
    {
        currentBossState.Update(this);
    }

    private void FixedUpdate()
    {
        currentBossState.FixedUpdate(this);
    }

    public void Transition(BossState targetState)
    {
        currentBossState.Exit(this);
        currentBossState = targetState;
        currentBossState.Entry(this);
    }
    
}
