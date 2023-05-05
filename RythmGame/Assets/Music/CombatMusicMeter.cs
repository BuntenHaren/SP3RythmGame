using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using FMODUnity;
using FMOD.Studio;

public class CombatMusicMeter : MonoBehaviour
{
    [ParamRef]
    [FormerlySerializedAs("parameter")]
    public string Parameter;

    [FormerlySerializedAs("value")]
    public float Value;

    public bool ParameterChange = false;

    public EnemiesInCombatCounter Counter;

    private bool Triggered = false;

    private float OldValue = 0;

    private PARAMETER_DESCRIPTION parameterDescription;
    public PARAMETER_DESCRIPTION ParameterDescription { get { return parameterDescription; } }

    private FMOD.RESULT Lookup()
    {
        FMOD.RESULT result = RuntimeManager.StudioSystem.getParameterDescriptionByName(Parameter, out parameterDescription);
        return result;
    }

    private void Awake()
    {
        if (string.IsNullOrEmpty(parameterDescription.name))
        {
            Lookup();
        }
    }

    public void Update()
    {
        ParameterChange = Counter.inCombat;

        if (ParameterChange == true)
        {
            if (Triggered == false)
            {
                Value = OldValue + 1;
                OldValue = Value;
                TriggerParameters();
                Triggered = true;
                Debug.Log("true,false,change");
            }
            if (Triggered == true)
            {
                Debug.Log("Returntrue2");
                return;
            }
        }
        if (ParameterChange == false)
        {
            if (Triggered == false)
            {
                return;
            }
            if (Triggered == true)
            {
                Triggered = false;
                Value = OldValue - 1;
                OldValue = Value;
                TriggerParameters();
            }
        }
    }

    public void TriggerParameters()
    {
        if (!string.IsNullOrEmpty(Parameter))
        {
            FMOD.RESULT result = RuntimeManager.StudioSystem.setParameterByID(parameterDescription.id, Value);
            if (result != FMOD.RESULT.OK)
            {
                RuntimeUtils.DebugLogError(string.Format(("[FMOD] StudioGlobalParameterTrigger failed to set parameter {0} : result = {1}"), Parameter, result));
            }
        }
    }
}
