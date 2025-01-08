using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Player Ability Container")]
    public class PlayerAbilityContainer : ScriptableObject
    {
        [SerializeField] private GameObject m_moneyTalkAbilityPrefab;
        [SerializeField] private GameObject m_bloodRageAbilityPrefab;
        
        public GameObject MoneyTalkAbilityPrefab => m_moneyTalkAbilityPrefab;
        public GameObject BloodRageAbilityPrefab => m_bloodRageAbilityPrefab;
    } 
}

