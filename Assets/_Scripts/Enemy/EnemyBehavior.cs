using SGGames.Scripts.Core;

namespace SGGames.Scripts.Enemies
{
    public class EnemyBehavior : EntityBehavior
    {
        public override void ToggleAllow(bool value)
        {
            m_isAllow = value;
        }
    }
}

