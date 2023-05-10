using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charm : ScriptableObject
{
    [Header("Base stuff for every charm")]
    public Sprite CharmSprite;
    [SerializeField]
    protected Health playerHealth;
    [SerializeField]
    protected JuiceCounter juiceCounter;
    [SerializeField]
    protected PlayerStats playerStats;
    [SerializeField]
    protected MusicEventPort beatPort;

    public virtual void Start()
    {
        PlayerAttacks.onPlayerAttackAction += OnPlayerAttackAction;
    }

    public virtual void OnPlayerAttackAction()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void Finish()
    {
        PlayerAttacks.onPlayerAttackAction -= OnPlayerAttackAction;
    }

}
