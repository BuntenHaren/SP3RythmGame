using UnityEngine;

namespace Bosses.States
{
    public class FirstPhaseState : BossState
    {
        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            health.onChange += CheckForEnrage;
        }

        private void CheckForEnrage(float amount)
        {
            Debug.Log(health.CurrentHealth);
            if(health.CurrentHealth <= health.CurrentMaxHealth * firstPhaseStats.ThresholdForEnrage)
                behaviour.Transition(new NextPhaseState());
        }

        public override void Exit()
        {
            base.Exit();
            health.onChange -= CheckForEnrage;
        }
    }
}
