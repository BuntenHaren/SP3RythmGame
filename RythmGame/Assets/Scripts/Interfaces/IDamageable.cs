using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float amount);
    public void TakeDamageOnBeat(float amount);
    public void HealDamage(float amount);
}
