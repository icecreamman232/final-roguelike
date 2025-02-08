using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum HealthConditionType
    {
        CurrentHealthCondition,
    }
    
    [CreateAssetMenu(fileName = "HealthConditionModifier", menuName = "SGGames/Modifiers/Health Condition",order = 8)]
    public class HealthConditionModifier : Modifier
    {
        public ComparisonType ComparisonType;
        public HealthConditionType HealthConditionType;
        public float ThresholdValue; //Percentage
        public Modifier[] ToTriggerModifiers;
    }
}

