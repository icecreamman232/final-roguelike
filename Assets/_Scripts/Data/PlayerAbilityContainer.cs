using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Player Ability Container")]
    public class PlayerAbilityContainer : ScriptableObject
    {
        [SerializeField] private GameObject m_moneyTalkAbilityPrefab;
        [SerializeField] private GameObject m_bloodRageAbilityPrefab;
        [SerializeField] private GameObject m_burnAuraAbilityPrefab;
        [SerializeField] private GameObject m_frozenAuraAbilityPrefab;
        [SerializeField] private GameObject m_poisonAuraAbilityPrefab;
        
        public GameObject MoneyTalkAbilityPrefab => m_moneyTalkAbilityPrefab;
        public GameObject BloodRageAbilityPrefab => m_bloodRageAbilityPrefab;
        public GameObject BurnAuraAbilityPrefab => m_burnAuraAbilityPrefab;
        public GameObject FrozenAuraAbilityPrefab => m_frozenAuraAbilityPrefab;
        public GameObject PoisonAuraAbilityPrefab => m_poisonAuraAbilityPrefab;
    } 
}

