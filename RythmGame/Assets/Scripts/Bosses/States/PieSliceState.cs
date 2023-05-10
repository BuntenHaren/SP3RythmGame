using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace Bosses.States
{
    public class PieSliceState : FirstPhaseState
    {
        private int numberOfBeatsWaited;
        private bool attackTelegraphStarted;
        private bool startedAttacking;
        private bool hasDamagedPlayer;
        private Mesh attackMesh;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            behaviour.ResetTelegraphPositions();
            attackMesh = new Mesh();
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
                    firstPhaseStats.PieSliceStartingOffset + firstPhaseStats.PieSliceSectorAngle * i + firstPhaseStats.PieSliceAngleBetweenSlices * i);
                combine[i].transform = behaviour.transform.localToWorldMatrix;
            }
            
            attackMesh.CombineMeshes(combine);
            behaviour.GenerateCircles[0].SetMesh(attackMesh);
            behaviour.GenerateCircles[0].GetComponent<MeshCollider>().sharedMesh = attackMesh;
            behaviour.GenerateCircles[0].transform.position = new Vector3(0, 0.05f, 0);

        }

        public override void Update()
        {
            
        }

        private void StartAttack()
        {
            startedAttacking = true;
            timer.StartTimer(2);
            RuntimeManager.PlayOneShot(firstPhaseStats.PieSliceSFX);
            behaviour.GenerateCircles[0].GetComponent<MeshCollider>().enabled = true;

        }

        protected override void TimerDone()
        {
            behaviour.Transition(new IdleFirstPhase());
        }

        public override void OnTriggerStay(Collider other)
        {
            if(hasDamagedPlayer || !startedAttacking)
                return;

            if(other.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(firstPhaseStats.PieSliceCircleDamage);
                hasDamagedPlayer = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            behaviour.GenerateCircles[0].SetMesh(new Mesh());
            behaviour.GenerateCircles[0].gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
