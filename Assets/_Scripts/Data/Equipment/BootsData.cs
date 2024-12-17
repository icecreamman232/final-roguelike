using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Boots",fileName = "BootsData")]
    public class BootsData : ItemData
    {
        [Header("Modifier")] 
        [SerializeField] private Modifier[] m_modifierList;
        
        public Modifier[] ModifierList => m_modifierList;
    }
}