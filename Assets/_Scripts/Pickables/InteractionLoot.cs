using SGGames.Scripts.Data;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class InteractionLoot : MonoBehaviour
    {
        [SerializeField] private DropsTableData m_dropsTableData;
        private readonly float m_dropRadius = 1.5f;
        private Transform m_spawnParent;
        /// <summary>
        /// Maximum times the system try to find the proper position to spawn the loot.
        /// If exceed this count, we just use the last postion;
        /// </summary>
        private readonly int m_maxCheckPosCount = 8;
        
        public void SpawnLoot(Transform spawnParent)
        {
            m_spawnParent = spawnParent;
            
            //Coin
            DropCurrency(m_dropsTableData.CoinPrefab,m_dropsTableData.CoinDropAmount);
            
            //Key
            DropCurrency(m_dropsTableData.KeyPrefab,m_dropsTableData.KeyDropAmount);
            
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
                randomSpawnPos = GetRandomDropPosition(m_dropRadius);
                Instantiate(prefab, randomSpawnPos, Quaternion.identity,m_spawnParent);
            }
        }
        
        private void DropItems()
        {
            var chance = Random.Range(0f, 100f);
            var prefab = m_dropsTableData.GetNextLoot(chance);
            if (prefab == null) return;
            Instantiate(prefab, GetRandomDropPosition(m_dropRadius), Quaternion.identity,m_spawnParent);
        }

        private Vector2 GetRandomDropPosition(float radius)
        {
            var lvlManager = LevelManager.Instance;
            var randomSpawnPos = Random.insideUnitCircle * Random.Range(radius/2,radius) + (Vector2)transform.position;
            var count = 0;
            while (!lvlManager.IsPositionInsideRoomBoundary(randomSpawnPos))
            {
                randomSpawnPos = Random.insideUnitCircle * Random.Range(radius/2,radius) + (Vector2)transform.position;
                count++;
                if (count >= m_maxCheckPosCount)
                {
                    break;
                }
            }
            return ClampSpawnPos(lvlManager.CurrentRoom.SpawnPivots, randomSpawnPos);
        }
        
        private Vector2 ClampSpawnPos((Vector2 botLeft,Vector2 topRight) roomPivot, Vector2 spawnPos)
        {
            var clampX = Mathf.Clamp(spawnPos.x,roomPivot.botLeft.x,roomPivot.topRight.x);
            var clampY = Mathf.Clamp(spawnPos.y,roomPivot.botLeft.y,roomPivot.topRight.y);
            return new Vector2(clampX, clampY);
        }
    }
}

