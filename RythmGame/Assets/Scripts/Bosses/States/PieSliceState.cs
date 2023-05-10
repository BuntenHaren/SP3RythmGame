using System.Collections.Generic;
using FMODUnity;
using Unity.IO.LowLevel.Unsafe;
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
        private GameObject[] telegraphs;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            behaviour.ResetTelegraphPositions();
            attackMesh = new Mesh();
            telegraphs = new GameObject[firstPhaseStats.PieSliceAmountOfSlices];
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
                telegraphs[i] = GameObject.Instantiate(behaviour.GenerateCircles[0].gameObject, behaviour.transform);
                attackMesh = behaviour.GenerateCircles[0].CreateCircleMesh(100,
                    firstPhaseStats.PieSliceRange, 
                    firstPhaseStats.PieSliceSectorAngle,
                    firstPhaseStats.PieSliceStartingOffset + firstPhaseStats.PieSliceSectorAngle * i + firstPhaseStats.PieSliceAngleBetweenSlices * i);
                //combine[i].transform = behaviour.transform.localToWorldMatrix;
                
                telegraphs[i].GetComponent<GenerateCircle>().SetMesh(attackMesh);
                telegraphs[i].GetComponent<MeshCollider>().sharedMesh = attackMesh;
                telegraphs[i].GetComponent<MeshCollider>().enabled = false;
                telegraphs[i].transform.localPosition = new Vector3(0, 0.05f, 0);
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
            for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
            {
                telegraphs[i].GetComponent<MeshCollider>().enabled = true;
            }
        }

        protected override void TimerDone()
        {
            behaviour.Transition(new IdleFirstPhase());
        }

        public override void OnCollisionStay(Collision other)
        {
            if(hasDamagedPlayer || !startedAttacking)
                return;
            
            if(other.gameObject.TryGetComponent(out PlayerHealth player))
            {
                hasDamagedPlayer = true;
                Debug.Log("Damaged player");
                player.TakeDamage(firstPhaseStats.PieSliceCircleDamage);
                for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
                {
                    telegraphs[i].GetComponent<MeshCollider>().enabled = false;
                }
            }
        }

        private void DestroyTelegraphs()
        {
            for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
            {
                GameObject.Destroy(telegraphs[i]);
            }
        }

        public override void Exit()
        {
            base.Exit();
            DestroyTelegraphs();
        }
    }
}
