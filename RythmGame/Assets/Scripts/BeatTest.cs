using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTest : MonoBehaviour
{
    public MusicEventPort beatPort;
    private MeshRenderer renderer;

    private void Start()
    {
        beatPort.onBeat += onBeat;
        renderer = GetComponent<MeshRenderer>();
    }

    private void onBeat()
    {
        renderer.enabled = !renderer.enabled;
    }
}
