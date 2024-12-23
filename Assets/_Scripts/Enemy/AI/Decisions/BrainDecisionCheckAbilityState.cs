using SGGames.Scripts.Abilities;
using UnityEngine;

namespace  SGGames.Scripts.Enemies
{
    public class BrainDecisionCheckAbilityState : BrainDecision
    {
        [SerializeField] private Ability m_ability;
        [SerializeField] private AbilityState m_stateToCheck;


        public override bool CheckDecision()
        {
            return m_ability.CurrentState == m_stateToCheck;
        }
    }
}

