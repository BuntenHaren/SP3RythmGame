using UnityEngine;

namespace Bosses.States
{
    public abstract class BossState
    {
        protected BossBehaviour behaviour;
        protected FirstPhaseStats firstPhaseStats;
        protected SecondPhaseState secondPhaseStats;
        protected MusicEventPort beatPort;
        protected Health health;
        protected Timer timer;
    
        public virtual void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseState secondPhase, Health bossHealth, MusicEventPort beatPort)
        {
            timer = new Timer();
            timer.TimerDone += TimerDone;
            behaviour = bossBehaviour;
            firstPhaseStats = firstPhase;
            secondPhaseStats = secondPhase;
            health = bossHealth;
            this.beatPort = beatPort;
            this.beatPort.onBeat += OnBeat;
        }

        public virtual void Update()
        {
        
        }

        public virtual void FixedUpdate()
        {
            timer.UpdateTimer(Time.fixedDeltaTime);
        }

        protected virtual void TimerDone()
        {
        
        }

        public virtual void OnBeat()
        {
        
        }

        public virtual void Exit()
        {
            timer.TimerDone -= TimerDone;
            beatPort.onBeat -= OnBeat;
        }
    
    }
}
