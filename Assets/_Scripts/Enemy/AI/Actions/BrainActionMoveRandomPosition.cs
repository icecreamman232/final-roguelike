
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionMoveRandomPosition : BrainAction
    {
        [SerializeField] private EnemyMovement m_movement;
        
        public override void DoAction()
        {
            var randDirection = UnityEngine.Random.insideUnitCircle;
            m_movement.SetDirection(randDirection);
        }
    }
}

