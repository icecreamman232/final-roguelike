using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    /// <summary>
    /// Check if enemy is getting hit by a source defined by layer mask.
    /// Could set player as target
    /// </summary>
    public class BrainDecisionGetHit : BrainDecision
    {
        [SerializeField] private LayerMask m_targetLayerMask;
        [SerializeField] private bool m_shouldSetPlayerAsTarget;
        private EnemyHealth m_health;
        private bool m_isHit;
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_health = m_brain.Owner.GetComponent<EnemyHealth>();
        }

        public override void OnEnterState()
        {
            m_health.OnHit += OnGettingHit;
            base.OnEnterState();
        }

        private void OnGettingHit(EnemyHitInfo hitInfo)
        {
            if(!LayerManager.IsInLayerMask(hitInfo.Source.layer, m_targetLayerMask)) return;
            m_isHit = true;
            if (m_shouldSetPlayerAsTarget)
            {
                m_brain.Target = LevelManager.Instance.PlayerRef.transform;
            }
        }

        public override bool CheckDecision()
        {
            return m_isHit;
        }

        public override void OnExitState()
        {
            m_isHit = false;
            m_health.OnHit -= OnGettingHit;
            base.OnExitState();
        }
    }
}

