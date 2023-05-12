namespace Bosses.States
{
    public class NextPhaseState : BossState
    {
        public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
            
        }

        public override void Exit()
        {
            base.Exit();
            behaviour.bossAnim.SetBool("Phase 2", true);
        }
    }
}
