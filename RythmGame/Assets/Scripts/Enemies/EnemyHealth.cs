using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    //Stats
    [SerializeField]
    private float maxHealth;
    private float health;

    //Visual effects
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private float damageColorTime;
    [SerializeField]
    private GameObject onBeatParticles;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private DeathPort deathPort;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamageOnBeat(float damage)
    {
        health -= damage;
        Instantiate(onBeatParticles);

        if (health <= 0)
        {
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            gameObject.SetActive(false);
        }
    }

    public void HealDamage(float damage)
    {
        health = health + damage;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private IEnumerator ChangeColor()
    {
        var originalColor = sr.color;
        sr.color = damageColor;
        yield return new WaitForSeconds(damageColorTime);
        sr.color = originalColor;
    }
}
