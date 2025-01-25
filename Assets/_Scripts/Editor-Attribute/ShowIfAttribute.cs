using System;
using UnityEngine;

namespace SGGames.Scripts.EditorExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionBoolean;
        public bool ShouldDisplay;
        
        public ShowIfAttribute(string conditionBoolean, bool shouldDisplay)
        {
            ConditionBoolean = conditionBoolean;
            ShouldDisplay = shouldDisplay;
        }
    }
}

