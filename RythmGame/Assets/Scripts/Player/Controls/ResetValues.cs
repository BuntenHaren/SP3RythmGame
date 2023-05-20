using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetValues : MonoBehaviour
{

    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private PassiveCharm emptyPassiveCharm;
    [SerializeField]
    private ActiveCharm emptyActiveCharm;


    // Start is called before the first frame update
    void Awake()
    {
        playerStats.ResetValues();

        GetComponent<ControlCharms>().SwitchPassiveCharm(emptyPassiveCharm);
        GetComponent<ControlCharms>().SwitchActiveCharm(emptyActiveCharm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
