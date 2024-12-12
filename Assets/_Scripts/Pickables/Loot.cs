using SGGames.Scripts.Data;
using SGGames.Scripts.Healths;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Pickables
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private EnemyHealth m_enemyHealth;
        [SerializeField] private DropsTableData m_dropsTableData;
        
        private readonly float m_dropRadius = 3f;

        private void Start()
        {
            m_enemyHealth.OnDeath += SpawnLoot;
        }
        
        private void OnDestroy()
        {
            m_enemyHealth.OnDeath -= SpawnLoot;
        }
        
        private void SpawnLoot()
        {
            //TODO:Check spawn to make sure drops not being spawn outside of the room
            
            //Coin
            DropCurrency(m_dropsTableData.CoinPrefab,m_dropsTableData.CoinDropAmount);
            
            //Small Exp
            DropCurrency(m_dropsTableData.SmallExpPrefab,m_dropsTableData.SmallExpDropAmount);
            
            //Big Exp
            DropCurrency(m_dropsTableData.BigExpPrefab,m_dropsTableData.BigExpDropAmount);

            DropItems();
        }

        private void DropCurrency(GameObject prefab, int amount)
        {
            if (amount <= 0) return;
            
            var randomSpawnPos = Vector2.zero;
            for (int i = 0; i < amount; i++)
            {
                randomSpawnPos = Random.insideUnitCircle * Random.Range(m_dropRadius/2,m_dropRadius);
                Instantiate(prefab, (Vector3)randomSpawnPos + transform.position, Quaternion.identity);
            }
        }
        
        private void DropItems()
        {
            //TODO:Implement drops item behavior    
        }
    }
}

