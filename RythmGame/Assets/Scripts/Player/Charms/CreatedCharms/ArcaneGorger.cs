using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/Arcane Gorger")]
public class ArcaneGorger : PassiveCharm
{
    private float JuiceLevelRatio = 0f;
    private float BeatAccuracy = 0f;

    private float BeatAccuracyMultiplier = 1f;
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
        //playerStats.CurrentMaxHealth *= 0.75;                         for when there is a variable for max health
    }

    public override void OnPlayerAttackAction()
    {
        // calculate juice gauge fill ratio
        JuiceLevelRatio = juiceCounter.CurrentJuice / juiceCounter.MaxJuice;

        DistanceToLastBeat = beatPort.GetDistanceToLastBeat();
        DistanceToNextBeat = beatPort.GetDistanceToNextBeat();

        
        // set BeatAccuracy to 1 of on beat, else 0
        if ((DistanceToLastBeat <= BeatWindow) || (DistanceToNextBeat <= BeatWindow))
        {
            BeatAccuracy = BeatAccuracyMultiplier;
        }
        else
        {
            BeatAccuracy = 0f;
        }

        // set heal multiplier
        playerStats.HealOnAttack = BaseMultiplier * BeatAccuracy * JuiceLevelRatio;

        Debug.Log("Beat accuracy: " + BeatAccuracy + 
            "\nBase multiplier: " + BaseMultiplier + 
            "\nCurrent juice: " + juiceCounter.CurrentJuice + 
            "\nJuice ratio: " + JuiceLevelRatio + 
            "\nJuice multiplier: " + JuiceLevelMultiplier + 
            "\nHeal amount: " + playerStats.HealOnAttack + 
            "\n");
    }
}
