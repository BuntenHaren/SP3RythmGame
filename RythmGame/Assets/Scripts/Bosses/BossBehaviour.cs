using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [HideInInspector]
    public List<GenerateCircle> GenerateCircles;

    [SerializeField]
    private BossStats firstPhaseStats;
    [SerializeField]
    private BossStats secondPhaseStats;
    [SerializeField]
    private Health bossHealth;
    [SerializeField]
    private MusicEventPort beatPort;
    
    private BossState currentBossState;

    private void Start()
    {
        currentBossState = new IdleFirstPhase();
        GenerateCircles = GetComponentsInChildren<GenerateCircle>().ToList();
        currentBossState.Entry(this, firstPhaseStats, secondPhaseStats, bossHealth);
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

    public Vector3 GetPlayerPos()
    {
        GameObject potentialPlayer = GameObject.FindWithTag("Player");
        if(potentialPlayer == null)
            return transform.position;
        return potentialPlayer.transform.position;
    }

    public void Transition(BossState targetState)
    {
        currentBossState.Exit();
        currentBossState = targetState;
        currentBossState.Entry(this, firstPhaseStats, secondPhaseStats, bossHealth);
    }

    private void OnDisable()
    {
        beatPort.onBeat -= OnBeat;
    }
}
