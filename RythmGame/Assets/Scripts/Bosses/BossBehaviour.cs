using System.Collections.Generic;
using System.Linq;
using Bosses.States;
using FMODUnity;
using UnityEngine;

namespace Bosses
{
    public class BossBehaviour : MonoBehaviour, IDamageable, IColliderListener
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
                circle.transform.rotation = transform.rotation;
                circle.SetMesh(new Mesh());
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
            if(bossHealth.Invurnerable)
                return;
            
            bossHealth.CurrentHealth -= amount;
            bossAnim.SetTrigger("Hurt");
            RuntimeManager.PlayOneShot(firstPhaseStats.HurtSFX);
            if(bossHealth.CurrentHealth <= 0)
                Transition(new DeathState());
        }

        public void TakeDamageOnBeat(float amount)
        {
            TakeDamage(amount);
        }

        public void HealDamage(float amount)
        {
            bossHealth.CurrentHealth += amount;
        }

        public void CollisionEnter(Collision collision)
        {
            currentBossState.OnCollisionEnter(collision);
        }

        public void TriggerEnter(Collider other)
        {
            currentBossState.OnTriggerEnter(other);
        }

        public void CollisionStay(Collision collision)
        {
            currentBossState.OnCollisionStay(collision);
        }

        public void TriggerStay(Collider other)
        {
            currentBossState.OnTriggerStay(other);
        }
    }
}
