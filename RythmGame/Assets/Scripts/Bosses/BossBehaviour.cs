using System.Collections.Generic;
using System.Linq;
using Bosses.States;
using FMODUnity;
using UnityEngine;

namespace Bosses
{
    public class BossBehaviour : MonoBehaviour, IDamageable
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

        public void ResetTelegraphPositions()
        {
            foreach(var circle in GenerateCircles)
            {
                circle.transform.position = transform.position;
            }
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

        public void TakeDamage(float amount)
        {
            bossHealth.CurrentMaxHealth -= amount;
            bossAnim.SetTrigger("Hurt");
            RuntimeManager.PlayOneShot(firstPhaseStats.HurtSFX);
            if(bossHealth.CurrentMaxHealth <= 0)
                Die();
        }

        public void TakeDamageOnBeat(float amount)
        {
            TakeDamage(amount);
        }

        public void HealDamage(float amount)
        {
            bossHealth.CurrentMaxHealth += amount;
        }

        private void Die()
        {
            bossAnim.SetTrigger("Death");
            RuntimeManager.PlayOneShot(firstPhaseStats.DeathSFX);
        }
    }
}
