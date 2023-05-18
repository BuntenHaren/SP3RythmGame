using UnityEngine;

namespace Bosses.States
{
    public class NextPhaseState : BossState
    {
        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            Debug.Log("Enraged");
            health.Invurnerable = true;
            timer.StartTimer(secondPhaseStats.EnragedTime);
            timer.TimerDone += () => behaviour.Transition(new IdleSecondPhase());
        }        

        public override void Exit()
        {
            base.Exit();
            behaviour.bossAnim.SetBool("Phase 2", true);
            health.Invurnerable = false;
        }
    }
}
