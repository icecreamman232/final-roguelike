using UnityEngine;

namespace SGGames.Scripts.Common
{
    /// <summary>
    /// Instantiate any object upon got called
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_prefabToSpawn;
        [SerializeField] private Quaternion m_spawnRotation;
        [SerializeField] private Transform m_spawnParent;
        [SerializeField] private float m_chanceToSpawn;

        /// <summary>
        /// Spawn using setting on the component
        /// </summary>
        public void Spawn()
        {
            var chance = Random.Range(0f, 100f);
            if (chance > m_chanceToSpawn) return;
            Instantiate(m_prefabToSpawn, transform.position, m_spawnRotation,m_spawnParent);
        }

        /// <summary>
        /// Spawn using passing setting
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        public void Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var chance = Random.Range(0f, 100f);
            if (chance <= m_chanceToSpawn) return;
            Instantiate(m_prefabToSpawn, position, rotation,parent);
        }
    }
}

