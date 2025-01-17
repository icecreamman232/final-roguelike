
namespace SGGames.Scripts.Weapons
{
    public class MeleeWeapon : Weapon
    {
        protected readonly string C_SWING_LEFT_ANIM_PARAM = "SwingLeft";
        protected readonly string C_SWING_RIGHT_ANIM_PARAM = "SwingRight";
        
        protected override void PlayUseWeaponAnimation()
        {
            base.PlayUseWeaponAnimation();
            if (m_animator != null)
            {
                m_animator.SetTrigger(m_isOnLeft ? C_SWING_LEFT_ANIM_PARAM : C_SWING_RIGHT_ANIM_PARAM);
            }
        }
    }
}

