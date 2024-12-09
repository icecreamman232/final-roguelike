using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainDecisionCheckTargetInRange : BrainDecision
    {
        [SerializeField] private float m_radius;
        [SerializeField] private LayerMask m_targetLayerMask;
        
        public override bool CheckDecision()
        {
            var target = Physics2D.OverlapCircle(transform.position, m_radius,m_targetLayerMask);
            if (target != null)
            {
                m_brain.Target = target.transform;
                return true;
            }

            return false;
        }
        
#if UNITY_EDITOR
        [SerializeField] private bool m_showDebugRange;
        private void OnDrawGizmos()
        {
            if (!m_showDebugRange) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
#endif
    }
}

