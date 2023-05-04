using FMODUnity;
using UnityEngine;

namespace Bosses
{
    [CreateAssetMenu(menuName = "RythmGame/Boss/FirstPhaseStats")]
    public class FirstPhaseStats : ScriptableObject
    {
        [field: Header("Circle Sector Attack Variables")]
        public int PieSliceAmountOfSlices;
        public float PieSliceRange;
        public float PieSliceSectorAngle;
        public float PieSliceCircleDamage;
        public float PieSliceStartingOffset;
        public float PieSliceAngleBetweenSlices;
        public int PieSliceAmountOfBeatsWarning;

        [field: Header("Hoof Stomp Attack Variables")]
        public float StompDamage;
        public int NumberOfStompsTotal;
        public int NumberOfBeatsWarningForStomp;
        public float StompRadius;
    
        [field: Header("Idle State Variables")]
        public float IdleTimeRangeMax;
        public float IdleTimeRangeMin;
    
    
        [field: Header("Thresholds")]
        public float ThresholdForEnrage;
    
        [field: Header("Health Multipliers")]
        public float HealingRecievedMultiplier = 1;
        public float DamageRecievedMultiplier = 1;

        [field: Header("SFX")] 
        public EventReference HoofStompSFX;
        public EventReference PieSliceSFX;
        public EventReference HurtSFX;
        public EventReference DeathSFX;
    }
}