using SGGames.Scripts.Healths;
using SGGames.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Common
{
    public class SpawnOnDeath : MonoBehaviour
    {
        [SerializeField] protected EnemyHealth m_health;
        [SerializeField] protected GameObject m_prefabToSpawn;
        [SerializeField] protected Vector2 m_spawnOffset;
        [SerializeField] protected float m_spawnRange;

        private readonly float m_offset = 0.5f;
        
        private void Start()
        {
            m_health.OnEnemyDeath += Spawn;
        }
        
        protected virtual void Spawn(EnemyHealth health)
        {
            var spawnPos = (Vector2)transform.position +  Random.insideUnitCircle * m_spawnRange + m_spawnOffset;

            if (!LevelManager.Instance.IsPositionInsideRoomBoundary(spawnPos))
            {
                spawnPos = ClampSpawnPos(LevelManager.Instance.CurrentRoom.SpawnPivots, spawnPos);
            }
            
            Instantiate(m_prefabToSpawn, spawnPos, Quaternion.identity,LevelManager.Instance.CurrentRoom.transform);
            m_health.OnEnemyDeath -= Spawn;
        }
        
        protected Vector2 ClampSpawnPos((Vector2 botLeft,Vector2 topRight) roomPivot, Vector2 spawnPos)
        {
            var clampX = Mathf.Clamp(spawnPos.x,roomPivot.botLeft.x + m_offset,roomPivot.topRight.x - m_offset);
            var clampY = Mathf.Clamp(spawnPos.y,roomPivot.botLeft.y + m_offset,roomPivot.topRight.y - m_offset);
            return new Vector2(clampX, clampY);
        }
    }
}

