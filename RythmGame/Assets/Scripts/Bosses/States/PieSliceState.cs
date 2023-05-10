using System;
using System.Collections.Generic;
using FMODUnity;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
        private GameObject telegraphHolder;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            behaviour.ResetTelegraphPositions();
            attackMesh = new Mesh();
            telegraphs = new GameObject[firstPhaseStats.PieSliceAmountOfSlices];
            telegraphHolder = new GameObject("TelegraphHolder");
            telegraphHolder.transform.parent = behaviour.transform;
            telegraphHolder.transform.localPosition = Vector3.zero;
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
            float angleTowardsPlayerOffset = Random.Range(0, firstPhaseStats.PieSliceMaxAngleDeviation * 2);
            
            for(int i = 0; i < firstPhaseStats.PieSliceAmountOfSlices; i++)
            {
                telegraphs[i] = Object.Instantiate(behaviour.GenerateCircles[0].gameObject, telegraphHolder.transform);
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
            
            float angleTowardsPlayer = GetAngleTowardsPlayerFromObject(telegraphHolder.transform);
            Debug.Log("Angle " + angleTowardsPlayer);
            angleTowardsPlayer += -firstPhaseStats.PieSliceMaxAngleDeviation + angleTowardsPlayerOffset;
            telegraphHolder.transform.Rotate(Vector3.up, angleTowardsPlayer);
            Debug.Log(telegraphHolder.transform.rotation.eulerAngles);
        }

        private float GetAngleTowardsPlayerFromObject(Transform obj)
        {
            Vector3 objRotation = obj.forward;
            Vector3 vectorTowardsPlayer = behaviour.GetPlayerPos() - obj.position;
            float ab = objRotation.x * vectorTowardsPlayer.x + objRotation.y * vectorTowardsPlayer.y + objRotation.z * vectorTowardsPlayer.z;
            Debug.Log("objRotation " + objRotation.magnitude);
            Debug.Log("vectortowards " + vectorTowardsPlayer.magnitude);
            float value = Mathf.Acos(ab/(objRotation.magnitude * vectorTowardsPlayer.magnitude));
            return value;
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
                player.TakeDamage(firstPhaseStats.PieSliceCircleDamage);
                hasDamagedPlayer = true;
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
            GameObject.Destroy(telegraphHolder);
        }

        public override void Exit()
        {
            base.Exit();
            DestroyTelegraphs();
        }
    }
}
