using System.Collections;
using System.Collections.Generic;
using Bosses;
using Bosses.States;
using FMODUnity;
using UnityEngine;

public class DeathState : SecondPhaseState
{
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        behaviour.bossAnim.SetTrigger("Death");
        RuntimeManager.PlayOneShot(secondPhase.DeathSFX);
        bossHealth.Invurnerable = true;
        GameObject.FindWithTag("Music").GetComponent<StudioGlobalParameterTrigger>().Value = 4;
    }
}
