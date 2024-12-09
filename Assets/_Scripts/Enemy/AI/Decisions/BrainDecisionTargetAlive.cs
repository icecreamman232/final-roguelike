using SGGames.Scripts.Healths;

namespace SGGames.Scripts.Enemies
{
    /// <summary>
    /// Check if current target is still alive
    /// </summary>
    public class BrainDecisionTargetAlive : BrainDecision
    {
        private Health m_targetHealth;
        public override bool CheckDecision()
        {
            if (m_targetHealth == null)
            {
                m_targetHealth = m_brain.Target.GetComponent<Health>();
            }

            return m_targetHealth.IsDead;
        }

        public override void OnExitState()
        {
            m_targetHealth = null;
            base.OnExitState();
        }
    }
}

