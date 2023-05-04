using System.Collections.Generic;
using System.Linq;
using Bosses.States;
using UnityEngine;

namespace Bosses
{
    public class BossBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public List<GenerateCircle> GenerateCircles;
        [HideInInspector]
        public Animator bossAnim;

        [SerializeField]
        private FirstPhaseStats firstPhaseStats;
        [SerializeField]
        private SecondPhaseStats secondPhaseStats;
        [SerializeField]
        private Health bossHealth;
        [SerializeField]
        private MusicEventPort beatPort;
    
        private BossState currentBossState;

        private void Start()
        {
            bossAnim = GetComponentInChildren<Animator>();
            currentBossState = new IdleFirstPhase();
            GenerateCircles = GetComponentsInChildren<GenerateCircle>().ToList();
            currentBossState.Entry(this, firstPhaseStats, secondPhaseStats, bossHealth, beatPort);
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
            currentBossState.Entry(this, firstPhaseStats, secondPhaseStats, bossHealth, beatPort);
        }

        private void OnDisable()
        {
            beatPort.onBeat -= OnBeat;
        }
    }
}
