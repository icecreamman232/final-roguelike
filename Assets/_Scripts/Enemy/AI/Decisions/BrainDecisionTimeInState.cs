using SGGames.Scripts.Attribute;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainDecisionTimeInState : BrainDecision
    {
        [SerializeField] private float m_minDuration;
        [SerializeField] private float m_maxDuration;
        [SerializeField][ReadOnly] private float m_duration;
        
        public override void OnEnterState()
        {
            base.OnEnterState();
            m_duration = Random.Range(m_minDuration, m_maxDuration);
        }

        public override bool CheckDecision()
        {
            return m_brain.TimeInState >= m_duration;
        }
    }
}

