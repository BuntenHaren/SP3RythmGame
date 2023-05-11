using UnityEngine;

public class ControlCharms : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    // private charm variables
    [Header("Charms")]
    // passive
    [SerializeField]
    private BeatMaster beatMaster;
    [SerializeField]
    private ArcaneGorger arcaneGorger;
    // active
    [SerializeField]
    private ArcaneSurge arcaneSurge;

    void Start()
    {
        playerStats.CurrentActiveCharm.Start();
        playerStats.CurrentPassiveCharm.Start();
    }

    void Update()
    {
        playerStats.CurrentActiveCharm.Update();
        playerStats.CurrentPassiveCharm.Update();
    }

    private void FixedUpdate()
    {
        playerStats.CurrentActiveCharm.FixedUpdate();
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

    private void OnActivateCharm()
    {
        playerStats.CurrentActiveCharm.ActivateCharm();
    }

    private void OnEnable()
    {
        playerStats.CurrentActiveCharm.Start();
        playerStats.CurrentPassiveCharm.Start();
    }

    private void OnDisable()
    {
        playerStats.CurrentActiveCharm.Finish();
        playerStats.CurrentPassiveCharm.Finish();
    }

    public void OnSwitchPassiveCharm()
    {
        if (playerStats.CurrentPassiveCharm == beatMaster)
        {
            SwitchPassiveCharm(arcaneGorger);
        }
        else
        if (playerStats.CurrentPassiveCharm == arcaneGorger)
        {
            SwitchPassiveCharm(beatMaster);
        }
    }
}