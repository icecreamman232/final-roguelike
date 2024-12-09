using SGGames.Scripts.Enemies;
using UnityEngine;


namespace SGGames.SCripts.Enemies
{
    /// <summary>
    /// Return true if enemy hit obstacle
    /// </summary>
    public class BrainDecisionHitObstacle : BrainDecision
    {
        [SerializeField] private EnemyMovement m_movement;
        [SerializeField] private bool m_isHitObstacle;
        private void OnEnable()
        {
            m_movement.OnHitObstacle += OnHitObstacle;
        }

        private void OnDisable()
        {
            m_movement.OnHitObstacle -= OnHitObstacle;
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
        }
    }
}

