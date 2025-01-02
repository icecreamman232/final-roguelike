
using SGGames.Scripts.Managers;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionSetPlayerAsTarget : BrainAction
    {
        public override void DoAction()
        {
            m_brain.Target = LevelManager.Instance.PlayerRef.transform;
        }
    }
}

