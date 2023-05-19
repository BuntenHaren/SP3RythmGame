using FMODUnity;
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
            behaviour.bossAnim.SetBool("Phase 2", true);
            behaviour.bossAnim.SetTrigger("PhaseChange");
            behaviour.MusicProgression(2);
        }

        protected override void TimerDone()
        {
            base.TimerDone();
            behaviour.Transition(new IdleSecondPhase());
        }

        public override void Exit()
        {
            base.Exit();
            health.Invurnerable = false;
        }
    }
}
