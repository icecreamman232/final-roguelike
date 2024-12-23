using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Accessories",fileName = "AccessoriesData")]
    public class AccessoriesData : ItemData
    {
        [Header("Modifier")] 
        [SerializeField] private Modifier[] m_modifierList;
        
        public Modifier[] ModifierList => m_modifierList;
    }
}