using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private float damageColorTime;

    [SerializeField]
    private Animator anim;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        //StartCoroutine(ChangeColor());

        if(health <= 0)
        {
            anim.SetBool("Dead", true);
            gameObject.SetActive(false);
        }
    }

    public void HealDamage(int damage)
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
