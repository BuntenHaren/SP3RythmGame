using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private float damageCD;
    private float timeSinceDamage;

    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private PlayerMovement playerMovement;

    void Awake()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        timeSinceDamage += Time.deltaTime;
    }

    public void PlayerTakeDamage(int damage)
    {
        if (timeSinceDamage > damageCD && playerMovement.isDashing == false)
        {
            timeSinceDamage = 0f;
            health = health - damage;
            healthBar.SetHealth(health);
            StartCoroutine(damageColorIEnum());
        }
    }

    private IEnumerator damageColorIEnum()
    {
        var originalColor = sr.color;
        sr.color = damageColor;
        yield return new WaitForSeconds(damageCD);
        sr.color = originalColor;
    }
}
