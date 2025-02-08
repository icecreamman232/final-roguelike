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
    }

    public enum ModifierType
    {
        MOVEMENT, HEALTH, DAMAGE,
        GAME_EVENT, ARMOR, COIN,
        PLAYER_EVENT, HEALING, ATTRIBUTE,
        MANA, CONVERT_MANA_TO_DAMAGE, HEALTH_CONDITION,
        WEAPON_TYPE_BASED, ATTACK_TIME,
        
        COUNT,
    }

    public enum PostTriggerModifierBehavior
    {
        SELF_REMOVED,
        NO_REMOVE,
    }

    public enum ComparisonType
    {
        Equal,
        EqualAndGreaterThan,
        GreaterThan,
        EqualAndLessThan,
        LessThan,
    }
}

