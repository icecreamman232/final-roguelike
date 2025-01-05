namespace SGGames.Scripts.Enemies
{
    public class BrainActionMoveRandomPosition : BrainAction
    {
        private EnemyMovement m_movement;

        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_movement = m_brain.Owner.gameObject.GetComponent<EnemyMovement>();
        }

        public override void DoAction()
        {
            var randDirection = UnityEngine.Random.insideUnitCircle;
            m_movement.SetDirection(randDirection.normalized);
        }
    }
}

