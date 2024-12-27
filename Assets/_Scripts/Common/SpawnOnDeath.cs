using SGGames.Scripts.Healths;
using SGGames.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Common
{
    public class SpawnOnDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth m_health;
        [SerializeField] private GameObject m_prefabToSpawn;
        [SerializeField] private Vector2 m_spawnOffset;
        [SerializeField] private float m_spawnRange;

        private void Start()
        {
            m_health.OnEnemyDeath += Spawn;
        }
        
        private void Spawn(EnemyHealth health)
        {
            var spawnPos = (Vector2)transform.position +  Random.insideUnitCircle * m_spawnRange + m_spawnOffset;
            Instantiate(m_prefabToSpawn, spawnPos, Quaternion.identity,LevelManager.Instance.CurrentRoom.transform);
            m_health.OnEnemyDeath -= Spawn;
        }
    }
}

