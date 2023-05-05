using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace Bosses.States
{
    public class HoofStompState : FirstPhaseState
    {
        private int numberOfBeatsWaited;
        private bool attackTelegraphStarted;
        private bool startedAttacking;
        private Vector3 attackPosition;
        private GenerateCircle outerRingTelegraph;
        private GenerateCircle innerCircleTelegraph;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            outerRingTelegraph = behaviour.GenerateCircles[0];
            innerCircleTelegraph = behaviour.GenerateCircles[1];
            behaviour.ResetTelegraphPositions();
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
            
            if(numberOfBeatsWaited == firstPhaseStats.NumberOfBeatsWarningForStomp - 1)
                behaviour.bossAnim.SetTrigger("HoofStomp");
            
            if(numberOfBeatsWaited >= firstPhaseStats.NumberOfBeatsWarningForStomp)
                StartAttack();
        }

        private void StartTelegraphAttack()
        {
            attackTelegraphStarted = true;
            attackPosition = behaviour.GetPlayerPos();
            attackPosition.y = 0.05f;
            outerRingTelegraph.transform.position = attackPosition;
            outerRingTelegraph.SetMesh(outerRingTelegraph.CreateHollowCircle(100, firstPhaseStats.StompRadius - 0.1f, firstPhaseStats.StompRadius, 360, 0));
            innerCircleTelegraph.transform.position = attackPosition;
        }

        public override void Update()
        {
            innerCircleTelegraph.SetMesh(innerCircleTelegraph.CreateCircleMesh(100, firstPhaseStats.StompRadius * (numberOfBeatsWaited / (float)firstPhaseStats.NumberOfBeatsWarningForStomp), 360, 0));
        }

        private void StartAttack()
        {
            startedAttacking = true;
            timer.StartTimer(2);
            outerRingTelegraph.SetMesh(new Mesh());
            
            RuntimeManager.PlayOneShot(firstPhaseStats.HoofStompSFX);
            
            Collider[] potentialHit = Physics.OverlapSphere(attackPosition, firstPhaseStats.StompRadius);
            foreach(Collider hit in potentialHit)
            {
                if(hit.gameObject.TryGetComponent<IDamageable>(out IDamageable damaged))
                {
                    damaged.TakeDamage(firstPhaseStats.StompDamage);
                }
            }

        }

        protected override void TimerDone()
        {
            behaviour.Transition(new IdleFirstPhase());
        }

        public override void Exit()
        {
            base.Exit();
            innerCircleTelegraph.SetMesh(new Mesh());
            outerRingTelegraph.SetMesh(new Mesh());
        }
    }
}
