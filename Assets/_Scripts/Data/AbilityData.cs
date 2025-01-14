using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Ability")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private SelectableAbility m_abilityID;
        [SerializeField] private string m_abilityName;
        [SerializeField] private GameObject m_abilityPrefab;
        
        public SelectableAbility AbilityID => m_abilityID;
        public string AbilityName => m_abilityName;
        public GameObject AbilityPrefab => m_abilityPrefab;
    }
}

