using UnityEngine;

public class ControlCharms : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    

    void Start()
    {
        //playerStats.CurrentActiveCharm.Start();
        playerStats.CurrentPassiveCharm.Start();
    }

    void Update()
    {
        //playerStats.CurrentActiveCharm.Update();
        playerStats.CurrentPassiveCharm.Update();
    }

    private void FixedUpdate()
    {
        //playerStats.CurrentActiveCharm.FixedUpdate();
        playerStats.CurrentPassiveCharm.FixedUpdate();
    }

    private void SwitchPassiveCharm(PassiveCharm newPassiveCharm)
    {
        playerStats.CurrentPassiveCharm.Finish();
        playerStats.CurrentPassiveCharm = newPassiveCharm;
        playerStats.CurrentPassiveCharm.Start();
        //Insert your thing here for switching the passive charm if you have something :)
        
    }
    
    private void SwitchActiveCharm(ActiveCharm newActiveCharm)
    {
        playerStats.CurrentActiveCharm.Finish();
        playerStats.CurrentActiveCharm = newActiveCharm;
        playerStats.CurrentActiveCharm.Start();
        //Insert your thing here for switching the active charm if you have something :)
        
    }

    private void OnEnable()
    {
        //playerStats.CurrentActiveCharm.Start();
        playerStats.CurrentPassiveCharm.Start();
    }

    private void OnDisable()
    {
        //playerStats.CurrentActiveCharm.Finish();
        playerStats.CurrentPassiveCharm.Finish();
    }
}
