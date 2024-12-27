

using MoreMountains.Tools;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Managers;
using SGGames.Scripts.Player;
using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class SpawnProjectileOnDeath : SpawnOnDeath
    {
        [SerializeField] private float m_angle;
        
        protected override void Spawn(EnemyHealth health)
        {
            var spawnPos = (Vector2)transform.position +  Random.insideUnitCircle * m_spawnRange + m_spawnOffset;

            if (!LevelManager.Instance.IsPositionInsideRoomBoundary(spawnPos))
            {
                spawnPos = ClampSpawnPos(LevelManager.Instance.CurrentRoom.SpawnPivots, spawnPos);
            }
            
            var projectileObj = Instantiate(m_prefabToSpawn, spawnPos, Quaternion.identity,LevelManager.Instance.CurrentRoom.transform);
            var projectile = projectileObj.GetComponent<Projectile>();
            
            var direction = MMMaths.RotateVector2(Vector2.right, m_angle);
            
            projectile.Spawn(transform.position, Quaternion.identity,direction, 
                new DamageInfo()
            {
                AdditionMaxDamage = 0,
                AdditionMinDamage = 0,
                CriticalDamage = 1,
                MultiplyDamage = 1,
            });
            
            m_health.OnEnemyDeath -= Spawn;
        }
    }
}

