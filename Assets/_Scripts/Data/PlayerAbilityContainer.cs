using System;
using System.Linq;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Player Ability Container")]
    public class PlayerAbilityContainer : ScriptableObject
    {
        [SerializeField] private AbilityData[] m_abilityContainer;

        public GameObject GetAbility(SelectableAbility abilityID)
        {
            var abilityData = m_abilityContainer.FirstOrDefault(x => x.AbilityID == abilityID);
            if (abilityData == null)
            {
                throw new Exception($"Ability ID {abilityID.ToString()} not found");
            }
            return abilityData.AbilityPrefab;
        }
    }
}

