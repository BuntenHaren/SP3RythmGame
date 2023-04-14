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
            //Find chosen script of gameobject with the chosen tag. Activate one thing inside of the script. Most likely tha attack or maybe a reference in the script that looks if doThing is false or true to get the chosen activity
            Debug.Log("Jippie");
            doThing = false;
        }
    }
}
