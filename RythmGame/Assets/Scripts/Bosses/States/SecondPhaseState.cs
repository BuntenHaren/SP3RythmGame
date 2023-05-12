using Bosses;
using Bosses.States;

public class SecondPhaseState : BossState
{
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        health.onChange += CheckForDeath;
    }

    private void CheckForDeath(float amount)
    {
        if(health.CurrentHealth <= 0)
            behaviour.Transition(new DeathState());
    }

    public override void Exit()
    {
        base.Exit();
        health.onChange -= CheckForDeath;
    }
}
