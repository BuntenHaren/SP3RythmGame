using UnityEngine;

namespace Bosses.States
{
    public class IdleFirstPhase : FirstPhaseState
    {
        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            timer.StartTimer(Random.Range(firstPhaseStats.IdleTimeRangeMin, firstPhase.IdleTimeRangeMax));
            behaviour.bossAnim.Play("Boss_Idle");
        }

        protected override void TimerDone()
        {
            float nextAttack = Random.Range(0f, 100f);
            if(nextAttack >= 50)
                behaviour.Transition(new HoofStompState());
            else
                behaviour.Transition(new PieSliceState());
        }
    }
}
