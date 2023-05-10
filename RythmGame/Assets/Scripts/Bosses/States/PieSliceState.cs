using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace Bosses.States
{
    public class PieSliceState : FirstPhaseState, IColliderListener
    {
        private int numberOfBeatsWaited;
        private bool attackTelegraphStarted;
        private bool startedAttacking;
        private bool hasDamagedPlayer;
        private GameObject[] telegraphs;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            behaviour.ResetTelegraphPositions();
            telegraphs = new GameObject[firstPhaseStats.PieSliceAmountOfSlices];
            
            for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
            {
                telegraphs[i] = GameObject.Instantiate(behaviour.GenerateCircles[0].gameObject, behaviour.transform);
                telegraphs[i].name = "Telegraph " + i;
            }
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
            
            for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
            {
                telegraphs[i].GetComponent<MeshFilter>().mesh = behaviour.GenerateCircles[0].CreateCircleMesh(100,
                    firstPhaseStats.PieSliceRange, 
                    firstPhaseStats.PieSliceSectorAngle,
                    firstPhaseStats.PieSliceStartingOffset * firstPhaseStats.PieSliceAngleBetweenSlices * i);
            }
            
            
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
            if(hasDamagedPlayer || !startedAttacking)
                return;

            if(collision.gameObject.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(firstPhaseStats.PieSliceCircleDamage);
                hasDamagedPlayer = true;
                Debug.Log("Damaged player");
            }
        }

        public void OnTriggerStay(Collider other)
        {

        }

        public override void Exit()
        {
            base.Exit();
            behaviour.GenerateCircles[0].SetMesh(new Mesh());
        }
    }
}
