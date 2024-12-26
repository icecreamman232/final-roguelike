using System.Collections;
using MoreMountains.Tools;
using SGGames.Scripts.Data;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class PatternRangedWeapon : RangedWeapon
    {
        [SerializeField] private ShootPatternData m_PatternData;

        public override void Shoot(Vector2 direction, DamageInfo damageInfo)
        {
            if (m_currentState != WeaponState.READY) return;
            StartCoroutine(ShootWithPattern(direction, damageInfo));
        }
        
        private IEnumerator ShootWithPattern(Vector2 direction, DamageInfo damageInfo)
        {
            m_currentState = WeaponState.SHOOTING;
            for (int i = 0; i < m_PatternData.ShootPattern.Length; i++)
            {
                yield return new WaitForSeconds(m_PatternData.ShootPattern[i].GetDelay());
                
                var projectileObj = m_projectilePooler.GetPooledGameObject();
                var projectile = projectileObj.GetComponent<Projectile>();
                
                var newDirection = direction;
                var rotation = Quaternion.identity;

                newDirection = MMMaths.RotateVector2(
                    m_PatternData.ShootPattern[i].IsRelative 
                        ? direction 
                        : Vector2.right, 
                    m_PatternData.ShootPattern[i].GetAngle());

                rotation = Quaternion.AngleAxis(Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg,Vector3.forward);
                
                projectile.Spawn(transform.position,rotation,newDirection,damageInfo);
                yield return null;
            }
            m_currentState = WeaponState.SHOT;
        }
    }
}
