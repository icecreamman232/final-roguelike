using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class PlayerAbilityController : MonoBehaviour, IGameService
    {
        [SerializeField] private PlayerAbilityContainer m_abilityContainer;
        private PlayerModifierHandler m_modifierHandler;
        
        private PlayerModifierHandler ModifierHandler
        {
            get
            {
                if (m_modifierHandler == null)
                {
                    m_modifierHandler = ServiceLocator.GetService<PlayerModifierHandler>();
                }
                return m_modifierHandler;
            }
        } 
        
        private void Start()
        {
            ServiceLocator.RegisterService<PlayerAbilityController>(this);
        }

        [ContextMenu("Add Ability")]
        private void TestAddAbility()
        {
            AddAbility(SelectableAbility.Blood_Rage);
        }

        public void AddAbility(SelectableAbility abilityID)
        {
            var abilityPrefab = m_abilityContainer.GetAbility(abilityID);
            var abilityGO = Instantiate(abilityPrefab,this.transform);
            var ability = abilityGO.GetComponent<ISelectableAbility>();
            ability.Initialize(ModifierHandler);
        }
    }
}

