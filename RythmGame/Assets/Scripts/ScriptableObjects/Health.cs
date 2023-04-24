using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Health")]
public class Health : ScriptableObject
{
    public UnityAction<float> onChange = delegate {};
    
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
