using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class TimelineBeat : MonoBehaviour
{
    public bool doThing = false;

    public string enemyTag;

    // Start is called before the first frame update
    void Start()
    {   
        GameObject.FindGameObjectsWithTag(enemyTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (doThing == true)
        {
            //Do specialized thing in script of enemy
            Debug.Log("Jippie");
            doThing = false;
        }
    }
}
