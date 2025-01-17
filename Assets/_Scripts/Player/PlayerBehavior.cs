using SGGames.Scripts.Core;

namespace SGGames.Scripts.Player
{
    public class PlayerBehavior : EntityBehavior
    {
        public override void ToggleAllow(bool value)
        {
            m_isAllow = value;
        }

        public virtual void OnPlayerFreeze(bool isFrozen)
        {
            
        }
    }
}

