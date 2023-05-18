using System;
using FMODUnity;
using UnityEngine;

namespace Bosses
{
    [CreateAssetMenu(menuName = "RythmGame/Boss/SecondPhaseStats")]
    public class SecondPhaseStats : ScriptableObject
    {
        [field: Header("Chances for attacks"), Range(0, 100)]
        public float ChanceForMinionSpawn;
        [field: Range(0, 100)]
        public float ChanceForStomp;
        [field: Range(0, 100)]
        public float ChanceForCircle;
        
        [field: Header("Circle Sector Attack Variables")]
        public int PieSliceAmountOfSlices;
        public float PieSliceRange;
        public float PieSliceSectorAngle;
        public float PieSliceCircleDamage;
        public float PieSliceAngleBetweenSlices;
        public int PieSliceAmountOfBeatsWarning;
        public float PieSliceMaxAngleDeviation;
        public Vector2 PieSliceOriginOffset;

        [field: Header("Hoof Stomp Attack Variables")]
        public float StompDamage;
        public int NumberOfStompsTotal;
        public int NumberOfBeatsWarningForStomp;
        public float StompRadius;
        public float StompShockwaveTime;
        public float StompShockwaveDamage;
        public float StompShockwaveFinalSize;
        public float StompShockwaveWidth;
    
        [field: Header("Idle State Variables")]
        public float IdleTimeRangeMax;
        public float IdleTimeRangeMin;
        public float EnragedTime;
    
        [field: Header("Health Multipliers")]
        public float HealingRecievedMultiplier = 1;
        public float DamageRecievedMultiplier = 1;

        [field: Header("SFX")] 
        public EventReference HoofStompSFX;
        public EventReference StompShockwaveSFX;
        public EventReference PieSliceSFX;
        public EventReference PieSliceTelegraphSFX;
        public EventReference MinionSpawnSFX;
        public EventReference HurtSFX;
        public EventReference DeathSFX;
    }
}
