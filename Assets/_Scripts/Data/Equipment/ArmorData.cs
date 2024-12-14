using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Armor",fileName = "ArmorData")]
    public class ArmorData : ItemData
    {
        [Header("Modifier")] 
        [SerializeField] private Modifier[] m_modifierList;
        [Header("Prefab")]
        [SerializeField] private GameObject m_pickerPrefab;
        
        public Modifier[] ModifierList => m_modifierList;
    }
}