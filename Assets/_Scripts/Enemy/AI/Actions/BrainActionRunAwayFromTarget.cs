using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionRunAwayFromTarget : BrainAction
    {
        [SerializeField] private EnemyMovement m_movement;
        
        public override void DoAction()
        {
            var directionToTarget = (m_brain.Target.position - transform.position).normalized;
            m_movement.SetDirection(directionToTarget * -1);
        }
    }
}

