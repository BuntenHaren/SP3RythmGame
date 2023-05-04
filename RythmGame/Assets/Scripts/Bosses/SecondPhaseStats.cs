using UnityEngine;

namespace Bosses
{
    [CreateAssetMenu(menuName = "RythmGame/Boss/SecondPhaseStats")]
    public class SecondPhaseStats : ScriptableObject
    {
        [field: Header("Circle Sector Attack Variables")]
        public int PieSliceAmountOfSlices;
        public float PieSliceRange;
        public float PieSliceSectorAngle;
        public float PieSliceCircleDamage;
        public float PieSliceStartingOffset;
        public float PieSliceAngleBetweenSlices;

        [field: Header("Hoof Stomp Attack Variables")]
        public float StompDamage;
        public int NumberOfStompsTotal;
        public int NumberOfBeatsWarningForStomp;
        public float StompRadius;
    
        [field: Header("Idle State Variables")]
        public float IdleTimeRangeMax;
        public float IdleTimeRangeMin;

        [field: Header("Health Multipliers")]
        public float HealingRecievedMultiplier = 1;
        public float DamageRecievedMultiplier = 1;
    }
}