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
    [SerializeField]
    private MusicEventPort beatPort;
    
    private BossState currentBossState;

    private void Start()
    {
        currentBossState.Entry(this, bossStats, bossHealth);
    }

    private void OnEnable()
    {
        beatPort.onBeat += OnBeat;
    }

    private void Update()
    {
        currentBossState.Update();
    }

    private void FixedUpdate()
    {
        currentBossState.FixedUpdate();
    }

    private void OnBeat()
    {
        currentBossState.OnBeat();
    }

    public void Transition(BossState targetState)
    {
        currentBossState.Exit();
        currentBossState = targetState;
        currentBossState.Entry(this, bossStats, bossHealth);
    }

    private void OnDisable()
    {
        beatPort.onBeat -= OnBeat;
    }
}
