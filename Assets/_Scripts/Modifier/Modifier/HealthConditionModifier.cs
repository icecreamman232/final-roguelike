using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum HealthConditionType
    {
        CurrentHealthCondition,
    }
    
    [CreateAssetMenu(fileName = "HealthConditionModifier", menuName = "SGGames/Modifiers/Health Condition")]
    public class HealthConditionModifier : Modifier
    {
        public ComparisonType ComparisonType;
        public HealthConditionType HealthConditionType;
        public float ThresholdValue; //Percentage
        public Modifier[] ToTriggerModifiers;
    }
}

