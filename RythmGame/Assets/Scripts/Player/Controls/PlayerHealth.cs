using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health healthObject;
    [SerializeField]
    private GameObject youDiedText;

    private bool Invurnerable = false;
    private Timer InvincibilityTimer;

    private void OnValidate()
    {
        if(healthObject == null)
            Debug.LogError("Health object is currently not set. Please set the Health Object to a viable object of type Health");
    }

    private void Start()
    {
        InvincibilityTimer = new Timer();
        InvincibilityTimer.TimerDone += MakeVurnerableAgain;
    }

    public void TakeDamage(int amount)
    {
        if(!Invurnerable)
        {
            healthObject.CurrentHealth -= amount;
            if(healthObject.CurrentHealth <= 0)
            {
                youDiedText.SetActive(true);
            }
        }
    }

    public void HealDamage(int amount)
    {
        healthObject.CurrentHealth += amount;
    }

    public void MakeInvurnerableForTime(float time)
    {
        Invurnerable = true;
        InvincibilityTimer.StartTimer(time);
    }

    private void MakeVurnerableAgain()
    {
        Invurnerable = false;
    }

    private void FixedUpdate()
    {
        InvincibilityTimer.UpdateTimer(Time.fixedDeltaTime);
    }
}
