using SGGames.Scripts.Data;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class PlayerAbilityController : MonoBehaviour
    {
        [SerializeField] private ModifierHandler m_modifierHandler;
        [SerializeField] private PlayerAbilityContainer m_abilityContainer;

        [ContextMenu("Add Ability")]
        private void TestAddAbility()
        {
            AddAbility(m_abilityContainer.BloodRageAbilityPrefab);
        }

        private void AddAbility(GameObject abilityPrefab)
        {
            var abilityGO = Instantiate(abilityPrefab,this.transform);
            var ability = abilityGO.GetComponent<ISelectableAbility>();
            ability.Initialize(m_modifierHandler);
        }
    }
}
