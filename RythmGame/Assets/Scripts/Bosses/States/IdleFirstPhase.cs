using UnityEngine;

namespace Bosses.States
{
    public class IdleFirstPhase : FirstPhaseState
    {
        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseState secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            timer.StartTimer(Random.Range(firstPhaseStats.IdleTimeRangeMin, firstPhase.IdleTimeRangeMax));
        }

        protected override void TimerDone()
        {
            int nextAttack = Random.Range(0, 2);
            if(nextAttack == 1)
                behaviour.Transition(new PieSliceState());
            else
                behaviour.Transition(new HoofStompState());
        }
    }
}
