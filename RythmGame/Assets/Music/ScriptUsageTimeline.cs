using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.Events;

public class ScriptUsageTimeline : MonoBehaviour
{
    class TimelineInfo
    {
        public int CurrentMusicBar = 0;
        public FMOD.StringWrapper LastMarker = new FMOD.StringWrapper();
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public FMODUnity.EventReference EventName;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance;

    private int previousBeat = 0;

    private int newBeatLimit = 0;

    [SerializeField] public UnityEvent trigger;

#if UNITY_EDITOR
        void Reset()
    {
        EventName = FMODUnity.EventReference.Find("event:/music/music");
    }
#endif

    void Start()
    {
        //Not original code
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(EventName);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    private void Update()
    {
        //Only original Code
        previousBeat = timelineInfo.CurrentMusicBar;   //Checking if previous beat is the same as current music beat in FMOD
        if(previousBeat > newBeatLimit)      //If previous beat is more than the newBeatLimit that is choosen of the previousBeat
        {
            //Do Thing you want to do on beat
            trigger.Invoke(); //Starts a Choosen UnityEvent object script
            Debug.Log(previousBeat + "Now");  //Test Debug log
            newBeatLimit = previousBeat;    //Changing newBeatLimit to the previousBeat
        }
        if(previousBeat < newBeatLimit)  //If previousBeat is lower than newBeatLimit, This is incase of loops in FMOD
        {
            //Do Thing you want to do on beat incase of looping in FMOD
            trigger.Invoke();    //Triggers the choosen UnityEvent object script
            newBeatLimit = previousBeat;   //Changing newBeatLimit to the previousBeat
        }
        //Check if changing unity event and invoke to other to reduce pulling and choosing scsripts
    }

    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.CurrentMusicBar, (string)timelineInfo.LastMarker));
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.CurrentMusicBar = parameter.bar;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.LastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}
