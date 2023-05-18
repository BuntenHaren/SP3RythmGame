using System.Collections;
using System.Collections.Generic;
using Bosses;
using UnityEngine;

public class MinionSpawnState : SecondPhaseState
{
    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        behaviour.Transition(new IdleSecondPhase());
    }
}
