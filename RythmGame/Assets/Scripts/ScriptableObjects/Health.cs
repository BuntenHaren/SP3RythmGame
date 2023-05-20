using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RythmGame/Health")]
public class Health : ScriptableObject
{
    public UnityAction<float> onChange = delegate {};

    [HideInInspector]
    public bool Invurnerable;
    
    public float BaseMaxHealth;
    [HideInInspector]
    public float CurrentMaxHealth;
    
    private float currentHealth;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if(Invurnerable)
                return;
            
            if(Math.Abs(currentHealth - value) > 0.05f)
            {
                float healthChange = value - currentHealth;
                if(value > CurrentMaxHealth)
                    return;
                currentHealth = value;
                onChange.Invoke(healthChange);
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = BaseMaxHealth;
        CurrentMaxHealth = BaseMaxHealth;
    }

    private void OnEnable()
    {
        ResetHealth();
    }
}
