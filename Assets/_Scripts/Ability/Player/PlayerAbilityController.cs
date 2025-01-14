using System;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class PlayerAbilityController : MonoBehaviour, IGameService
    {
        [SerializeField] private PlayerModifierHandler m_modifierHandler;
        [SerializeField] private PlayerAbilityContainer m_abilityContainer;

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
            ability.Initialize(m_modifierHandler);
        }
    }
}

