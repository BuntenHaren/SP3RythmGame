using System;
using System.Runtime.InteropServices;
using AOT;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MusicEventManager : MonoBehaviour
{
    class TimelineInfo
    {
        public int CurrentMusicBar = 0;
        public StringWrapper LastMarker = new StringWrapper();
    }

    [SerializeField]
    private MusicEventPort musicEventPort;
    
    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public EventReference EventName;

    EVENT_CALLBACK beatCallback;
    EventInstance musicInstance;

    private int previousBeat = 0;

    private int newBeatLimit = 0;

#if UNITY_EDITOR
    void Reset()
    {
        EventName = EventReference.Find("event:/music/music");
    }
#endif
    
    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.CurrentMusicBar, (string)timelineInfo.LastMarker));
    }

    private void Start()
    {
        //Not original code
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new EVENT_CALLBACK(BeatEventCallback);

        musicInstance = RuntimeManager.CreateInstance(EventName);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, EVENT_CALLBACK_TYPE.TIMELINE_BEAT | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    public void ChangeMusicImmediate(EventReference eventName)
    {
        musicInstance.stop(STOP_MODE.IMMEDIATE);
        musicInstance = RuntimeManager.CreateInstance(eventName);
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        musicInstance.setCallback(beatCallback, EVENT_CALLBACK_TYPE.TIMELINE_BEAT | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }
    
    //Doesn't work for the moment and I can't be arsed to understand the fade out time rn so...
    public void ChangeMusicAllowFadeOut(EventReference eventName, float fadeTime)
    {
        musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
        musicInstance = RuntimeManager.CreateInstance(eventName);
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        musicInstance.setCallback(beatCallback, EVENT_CALLBACK_TYPE.TIMELINE_BEAT | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    private void Update()
    {
        //Only original Code
        previousBeat = timelineInfo.CurrentMusicBar;
        
        //If previous beat is more than the newBeatLimit that is choosen of the previousBeat
        if(previousBeat > newBeatLimit)      
        {
            musicEventPort.onBeat.Invoke();
            newBeatLimit = previousBeat;
        }
        //If previousBeat is lower than newBeatLimit, This is incase of loops in FMOD
        else if(previousBeat < newBeatLimit)  
        {
            musicEventPort.onBeat.Invoke();
            newBeatLimit = previousBeat;
        }
        
    }

    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }
    
    [MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    static RESULT BeatEventCallback(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        EventInstance instance = new EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != RESULT.OK)
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
                case EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.CurrentMusicBar = parameter.bar;
                    }
                    break;
                case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.LastMarker = parameter.name;
                    }
                    break;
            }
        }
        return RESULT.OK;
    }
}