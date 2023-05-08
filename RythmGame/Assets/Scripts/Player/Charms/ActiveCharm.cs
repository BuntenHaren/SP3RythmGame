using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/ActiveCharm")]
public abstract class ActiveCharm : Charm
{
    [SerializeField]
    protected float activationCost;
    [SerializeField]
    protected float activeDuration;

    protected Timer activationTimer;

    public override void Start()
    {
        activationTimer = new Timer();
        base.Start();
    }

    public virtual void ActivateCharm()
    {
        activationTimer.StartTimer(activeDuration);
        activationTimer.TimerDone = delegate()
        { 
            EndActivation(); 
        };
        //Insert your SFX below this comment for the charm activation and probably start playing the active SFX as well :)
        
    }

    protected virtual bool CheckIfCanActivate()
    {
        return activationCost <= juiceCounter.CurrentJuice;
    }

    public override void Update()
    {
        activationTimer.UpdateTimer(Time.deltaTime);
    }

    protected virtual void EndActivation()
    {
        
    }
    
}
