using System.Collections;
using System.Collections.Generic;
using Bosses;
using Bosses.States;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathState : SecondPhaseState
{
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        behaviour.bossAnim.SetTrigger("Death");
        RuntimeManager.PlayOneShot(secondPhase.DeathSFX);
        bossHealth.Invurnerable = true;
        behaviour.MusicProgression(3);
        timer.StartTimer(secondPhaseStats.DeathTime);
    }

    protected override void TimerDone()
    {
        base.TimerDone();
        if(secondPhaseStats.SceneToLoadAfterDeath != null)
            SceneManager.LoadScene(secondPhaseStats.SceneToLoadAfterDeath);
    }
}
