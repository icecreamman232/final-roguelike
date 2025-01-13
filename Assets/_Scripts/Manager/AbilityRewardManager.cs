using System;
using System.Collections.Generic;
using System.Linq;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class AbilityRewardManager : MonoBehaviour, IGameService
    {
        [SerializeField] private PlayerAbilityContainer m_abilityContainer;
        private List<SelectableAbility> m_abilityIDList;
        
        private void Start()
        {
            ServiceLocator.RegisterService<AbilityRewardManager>(this);
            m_abilityIDList = new List<SelectableAbility>();
            foreach (var abilityID in Enum.GetValues(typeof(SelectableAbility)))
            {
                m_abilityIDList.Add((SelectableAbility)abilityID);
            }
            
            ShuffleAbilityIDList();
        }

        private void ShuffleAbilityIDList()
        {
            List<SelectableAbility> enumValues = Enum.GetValues(typeof(SelectableAbility)).Cast<SelectableAbility>().ToList();
            System.Random rng = new System.Random();

            // Fisher-Yates shuffle algorithm
            int n = enumValues.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (enumValues[k], enumValues[n]) = (enumValues[n], enumValues[k]);
            }
            m_abilityIDList = new List<SelectableAbility>(enumValues);
        }

        public SelectableAbility GetRandomAbility()
        {
            var result = m_abilityIDList[0];
            m_abilityIDList.RemoveAt(0);
            return result;
        }

        [ContextMenu("Test")]
        private void Test()
        {
            for (int i = 0; i < 3; i++)
            {
                var ability = GetRandomAbility();
                Debug.Log(ability);
            }
        }
    }
}

