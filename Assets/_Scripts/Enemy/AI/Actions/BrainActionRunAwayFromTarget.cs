namespace SGGames.Scripts.Enemies
{
    public class BrainActionRunAwayFromTarget : BrainAction
    {
        private EnemyMovement m_movement;
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_movement = m_brain.Owner.GetComponent<EnemyMovement>();
        }

        public override void DoAction()
        {
            var directionToTarget = (m_brain.Target.position - transform.position).normalized;
            m_movement.SetDirection(directionToTarget * -1);
        }
    }
}

