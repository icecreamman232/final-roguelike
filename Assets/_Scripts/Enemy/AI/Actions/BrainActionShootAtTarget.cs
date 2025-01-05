using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionShootAtTarget : BrainAction
    {
        private EnemyWeaponHandler m_weaponHandler;
        
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            m_weaponHandler = m_brain.Owner.gameObject.GetComponent<EnemyWeaponHandler>();
        }
        
        public override void DoAction()
        {
            m_weaponHandler.UseWeaponAtTarget(m_brain.Target);
        }
    }
}

