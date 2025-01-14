using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Ability")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private SelectableAbility m_abilityID;
        [SerializeField] private string m_abilityName;
        [SerializeField] private string m_abilityDesc;
        [SerializeField] private GameObject m_abilityPrefab;
        [SerializeField] private Sprite m_abilityIcon;
        
        public SelectableAbility AbilityID => m_abilityID;
        public string AbilityName => m_abilityName;
        public string AbilityDesc => m_abilityDesc;
        public GameObject AbilityPrefab => m_abilityPrefab;
        public Sprite AbilityIcon => m_abilityIcon;
    }
}

