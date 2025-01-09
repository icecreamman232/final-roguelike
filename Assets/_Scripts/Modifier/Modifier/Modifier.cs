using System;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class Modifier : ScriptableObject
    {
        [TextArea]
        public string Description;
        [Header("Data")]
        public ModifierType ModifierType;
        public float Duration;
        public bool InstantTrigger;
        public PostTriggerModifierBehavior PostTriggerBehavior;
        [Header("Runtime Data")] 
        public bool IsRunning;

        private void OnDisable()
        {
            IsRunning = false;
        }
    }

    public enum ModifierType
    {
        MOVEMENT,
        HEALTH,
        DAMAGE,
        GAME_EVENT,
        ARMOR,
        COIN,
        PLAYER_EVENT,
        HEALING,
        ATTRIBUTE,
        MANA,
        CONVERT_MANA_TO_DAMAGE,
        
        COUNT,
    }

    public enum PostTriggerModifierBehavior
    {
        SELF_REMOVED,
        NO_REMOVE,
    }

    public enum ValueType
    {
        Number,
        Percentage,
    }
}

