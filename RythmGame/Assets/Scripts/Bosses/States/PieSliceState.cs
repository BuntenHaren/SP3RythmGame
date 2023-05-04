using UnityEngine;

namespace Bosses.States
{
    public class PieSliceState : FirstPhaseState
    {
        private int numberOfBeatsWaited;
        private bool attackTelegraphStarted;
        private bool startedAttacking;
        private Mesh attackTelegraphMesh;

        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            attackTelegraphMesh = new Mesh();
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
        
            if(numberOfBeatsWaited >= firstPhaseStats.NumberOfBeatsWarningForStomp)
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
                combine[i].transform = behaviour.GenerateCircles[0].transform.worldToLocalMatrix;
            }
            
            Debug.Log(combine);
            
            attackTelegraphMesh.CombineMeshes(combine);
            attackTelegraphMesh.name = "Telegraph mesh";
            behaviour.GenerateCircles[0].SetMesh(attackTelegraphMesh);
        }

        public override void Update()
        {
            
        }

        private void StartAttack()
        {
            startedAttacking = true;
            timer.StartTimer(3);
        
        

        }

        protected override void TimerDone()
        {
            behaviour.Transition(new IdleFirstPhase());
        }
    }
}
