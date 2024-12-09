using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionSetFollowingTarget : BrainAction
    {
        [SerializeField] private EnemyMovement m_movement;
        public override void DoAction()
        {
            m_movement.SetToFollowingTarget(m_brain.Target);
        }
    }
}

