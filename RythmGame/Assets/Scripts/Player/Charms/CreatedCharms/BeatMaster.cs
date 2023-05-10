using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/Beat Master")]
public class BeatMaster : PassiveCharm
{
    private float JuiceLevelRatio = 0f;
    private float BeatAccuracy = 0f;

    private float BeatAccuracyMultiplier = 0.5f;
    private float BaseMultiplier = 0.5f;
    private float JuiceLevelMultiplier = 1f;

    private float DistanceToLastBeat = 0f;
    private float DistanceToNextBeat = 0f;
    private float BeatWindow;

    public override void Start()
    {
        base.Start();

        // get beat window 
        BeatWindow = playerStats.CurrentTimeForBeatWindow;
    }

    public override void OnPlayerAttackAction()
    {
        base.OnPlayerAttackAction();

        // calculate juice gauge fill ratio
        JuiceLevelRatio = juiceCounter.CurrentJuice / juiceCounter.MaxJuice;

        DistanceToLastBeat = beatPort.GetDistanceToLastBeat();
        DistanceToNextBeat = beatPort.GetDistanceToNextBeat();

        
        // add flat damage if any beat is within beat window
        if ((DistanceToLastBeat <= BeatWindow) || (DistanceToNextBeat <= BeatWindow))
        {
            BeatAccuracy = BeatAccuracyMultiplier;
        }
        else
        {
            BeatAccuracy = 0f;
        }

        // set damage multiplier
        playerStats.CurrentAttackDamageMultiplier = BaseMultiplier + JuiceLevelMultiplier * JuiceLevelRatio + BeatAccuracy;

        Debug.Log("Beat accuracy: " + BeatAccuracy + 
            "\nBase multiplier: " + BaseMultiplier + 
            "\nCurrent juice: " + juiceCounter.CurrentJuice + 
            "\nJuice ratio: " + JuiceLevelRatio + 
            "\nJuice multiplier: " + JuiceLevelMultiplier + 
            "\nAttack damage multiplier: " + playerStats.CurrentAttackDamageMultiplier + 
            "\n");
    }
}
