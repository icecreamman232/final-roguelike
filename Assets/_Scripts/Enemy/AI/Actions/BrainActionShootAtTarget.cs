using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionShootAtTarget : BrainAction
    {
        [SerializeField] private EnemyWeaponHandler m_weaponHandler;
        public override void DoAction()
        {
            m_weaponHandler.UseWeaponAtTarget(m_brain.Target);
        }
    }
}

