
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public enum ManaModifierType
    {
        Modify_Max_Mana,
        Modify_Current_Mana,
        Modify_Mana_Regenerate,
        
        Modify_Max_Mana_For_Duration,
        Modify_Current_Mana_For_Duration,
        Modify_Mana_Regenerate_For_Duration,
    }
    
    [CreateAssetMenu(fileName = "Mana Modifier", menuName = "SGGames/Modifiers/Mana",order = 10)]
    public class ManaModifier : Modifier
    {
        public ManaModifierType ManaModifierType;
        public float ModifierValue;
        public bool IsPercentValue;
    }
}

