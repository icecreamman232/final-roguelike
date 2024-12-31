using SGGames.Scripts.Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGGames.Scripts.StatusEffects
{
    public enum StatusEffectType
    {
        Burn,
        Poison,
        Freeze,
        Shock,
    }

    public enum DamageStackingType
    {
        Addition,
        Multiplication,
    }
    
    public class StatusEffectData : ScriptableObject
    {
        [Header("Base")]
        public StatusEffectType StatusEffectType;
        public float InitialDamage;
        public float MinDamage;
        public float MaxDamage;
        [Header("Stacking")]
        public bool Stackable;
        public DamageStackingType DamageStackingType;
        public float PercentPerStack;
        public int MaxStack;
        [Header("Time")]
        public float Duration;
        /// <summary>
        /// The delay before the effect cause to entity.
        /// Ex: Burn tick time is 1.5 then the entity will take damage from burn every 1.5s
        /// </summary>
        public float TickTime;
        [Header("Modifiers")]
        public Modifier[] Modifiers;
    }
}

