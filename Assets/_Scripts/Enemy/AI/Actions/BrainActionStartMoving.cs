using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionStartMoving : BrainAction
    {
        [SerializeField] private EnemyMovement m_movement;
        public override void DoAction()
        {
            m_movement.StartMoving();
        }
    }
}

