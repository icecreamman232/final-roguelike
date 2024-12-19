using MoreMountains.Tools;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class PatternRangedWeapon : RangedWeapon
    {
        [SerializeField] private ShootPatternData m_PatternData;

        public override void Shoot(Vector2 direction,
            (float additionDamage, float multiplierDamage, float criticalDamage) damageInfo = default)
        {
            for (int i = 0; i < m_PatternData.ShootPattern.Length; i++)
            {
                var projectileObj = m_projectilePooler.GetPooledGameObject();
                var projectile = projectileObj.GetComponent<Projectile>();
                
                var newDirection = direction;
                var rotation = Quaternion.identity;

                newDirection = MMMaths.RotateVector2(
                    m_PatternData.ShootPattern[i].IsRelative 
                        ? direction 
                        : Vector2.right, 
                    m_PatternData.ShootPattern[i].Angle);

                rotation = Quaternion.AngleAxis(Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg,Vector3.forward);

                projectile.Spawn(transform.position,rotation,newDirection,damageInfo);
                m_currentState = WeaponState.SHOT;
            }
        }
    }
}
