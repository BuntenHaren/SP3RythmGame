using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/ActiveEmptyCharm")]
public class ActiveEmptyCharm : ActiveCharm
{
    public override void ActivateCharm()
    {
        base.ActivateCharm();
    }

    protected override void EndActivation()
    {
        base.EndActivation();
    }
}