using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RythmGame/Health")]
public class Health : ScriptableObject
{
    public UnityAction<float> onChange = delegate {};

    [HideInInspector]
    public bool Invurnerable;
    
    public float MaxHealth;
    
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
                currentHealth = value;
                onChange.Invoke(healthChange);
            }
        }
    }

    private void OnEnable()
    {
        currentHealth = MaxHealth;
    }
}
