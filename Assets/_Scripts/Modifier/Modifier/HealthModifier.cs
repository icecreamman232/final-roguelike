
using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public enum HealthModifierType
    {
        IncreaseCurrentHP_ForDuration,
        DecreaseCurrentHP_ForDuration,
        OverrideCurrentHP_ForDuration,
        
        IncreaseMaxHP,
        DecreaseMaxHP,
        OverrideMaxHP,
    }
    
    
    [CreateAssetMenu(fileName = "HealthModifier", menuName = "SGGames/Modifiers/Health Modifier")]
    public class HealthModifier : Modifier
    {
        public HealthModifierType HealthModifierType;
        public float ModifierValue;
    }
}

