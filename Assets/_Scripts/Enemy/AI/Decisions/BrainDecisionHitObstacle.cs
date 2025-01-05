using SGGames.Scripts.Enemies;
using UnityEngine;


namespace SGGames.SCripts.Enemies
{
    /// <summary>
    /// Return true if enemy hit obstacle
    /// </summary>
    public class BrainDecisionHitObstacle : BrainDecision
    {
        [SerializeField] private bool m_isHitObstacle;
        
        private EnemyMovement m_movement;
        
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_movement = m_brain.Owner.gameObject.GetComponent<EnemyMovement>();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_movement.OnHitObstacle += OnHitObstacle;
        }
        
        private void OnHitObstacle()
        {
            m_isHitObstacle = true;
        }

        public override bool CheckDecision()
        {
            return m_isHitObstacle;
        }

        public override void OnExitState()
        {
            base.OnExitState();
            m_isHitObstacle = false;
            m_movement.OnHitObstacle -= OnHitObstacle;
        }
    }
}

