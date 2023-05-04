using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using FMOD.Studio;
using FMODUnity;

public class JohanParameter : MonoBehaviour
{
    [ParamRef]
    [FormerlySerializedAs("parameter")]
    public string Parameter;

    [FormerlySerializedAs("value")]
    public float Value;

    public bool ParameterChange = false;

    public EnemiesInCombatCounter Counter;

    //public EmitterGameEvent TriggerEvent;

    private bool Triggered = false;
    private bool SecondTrigger = false;

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
                OldValue = Value;
                Value = OldValue + 1;
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
            if (SecondTrigger == false)
            {
                Triggered = false;
                Value = OldValue - 1;
                SecondTrigger = true;
                Debug.Log("False,false,reset");
            }
            if (SecondTrigger == true)
            {
                Debug.Log("Returntrue1");
                return;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (ParameterChange == true)
        {
            OldValue = Value;
            Value = OldValue + 1;
            TriggerParameters();
        }
        if (ParameterChange == false)
        {
            OldValue = Value;
            Value = OldValue + 2;
            TriggerParameters();
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