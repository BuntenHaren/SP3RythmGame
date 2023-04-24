using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charm : ScriptableObject
{
    [Header("Base stuff for every charm")]
    [SerializeField]
    protected Health playerHealth;
    [SerializeField]
    protected JuiceCounter juiceCounter;
    [SerializeField]
    protected PlayerStats playerStats;

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void Finish()
    {
        
    }
    
}
