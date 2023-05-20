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

    public CharmDescriptions CharmDescription;

    public bool isActive;

    public override void Start()
    {
        activationTimer = new Timer();
        activationTimer.TimerDone += EndActivation;
        base.Start();
    }

    public virtual void ActivateCharm()
    {
        activationTimer.StartTimer(activeDuration);

        isActive = true;
        //Insert your SFX below this comment for the charm activation and probably start playing the active SFX as well :)
        
    }

    protected virtual bool CheckIfCanActivate()
    {
        return activationCost <= juiceCounter.CurrentJuice;
    }

    public override void Update()
    {
        if (isActive)
        {
            activationTimer.UpdateTimer(Time.deltaTime);
        }
    }

    protected virtual void EndActivation()
    {
        isActive = false;
    }
    
}
