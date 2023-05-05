using FMODUnity;
using UnityEngine;

namespace Bosses.States
{
    public class PieSliceState : FirstPhaseState, IColliderListener
    {
        private int numberOfBeatsWaited;
        private bool attackTelegraphStarted;
        private bool startedAttacking;
        private Mesh attackTelegraphMesh;
        private bool hasDamagedPlayer;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            attackTelegraphMesh = new Mesh();
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
            
            if(numberOfBeatsWaited == firstPhaseStats.PieSliceAmountOfBeatsWarning - 4)
                behaviour.bossAnim.SetTrigger("GroundToss");
        
            if(numberOfBeatsWaited >= firstPhaseStats.PieSliceAmountOfBeatsWarning)
                StartAttack();
        }

        private void StartTelegraphAttack()
        {
            attackTelegraphStarted = true;
            
            
            CombineInstance[] combine = new CombineInstance[firstPhaseStats.PieSliceAmountOfSlices];
            
            for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
            {
                combine[i].mesh = behaviour.GenerateCircles[0].CreateCircleMesh(100,
                    firstPhaseStats.PieSliceRange, 
                    firstPhaseStats.PieSliceSectorAngle,
                    firstPhaseStats.PieSliceStartingOffset * firstPhaseStats.PieSliceAngleBetweenSlices);
                combine[i].transform = behaviour.transform.localToWorldMatrix;
            }
            
            attackTelegraphMesh.CombineMeshes(combine);
            attackTelegraphMesh.name = "Telegraph mesh";
            behaviour.GenerateCircles[0].SetMesh(attackTelegraphMesh);
            behaviour.GenerateCircles[0].transform.position = behaviour.transform.position;
        }

        public override void Update()
        {
            
        }

        private void StartAttack()
        {
            startedAttacking = true;
            timer.StartTimer(2);
            RuntimeManager.PlayOneShot(firstPhaseStats.PieSliceSFX);
        }

        protected override void TimerDone()
        {
            behaviour.Transition(new IdleFirstPhase());
        }

        public void OnCollisionEnter(Collision collision)
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            
        }

        public void OnCollisionStay(Collision collision)
        {
            
        }

        public void OnTriggerStay(Collider other)
        {
            if(hasDamagedPlayer)
                return;

            if(other.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(firstPhaseStats.PieSliceCircleDamage);
                hasDamagedPlayer = true;
            }
        }
    }
}
