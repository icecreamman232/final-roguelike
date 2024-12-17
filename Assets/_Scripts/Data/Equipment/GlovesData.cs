using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Gloves",fileName = "GlovesData")]
    public class GlovesData : ItemData
    {
        [Header("Modifier")] 
        [SerializeField] private Modifier[] m_modifierList;

        public Modifier[] ModifierList => m_modifierList;
    }
}