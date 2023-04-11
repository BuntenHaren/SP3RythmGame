using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Health")]
public class Health : ScriptableObject
{
    public UnityAction<int> onChange = delegate {  };
    
    public int MaxHealth;
    
    private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (currentHealth != value)
            {
                int healthChange = value - currentHealth;
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
