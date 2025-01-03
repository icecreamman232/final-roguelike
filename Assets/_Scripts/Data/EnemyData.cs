using SGGames.Scripts.Damages;
using SGGames.Scripts.Enemies;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Weapons;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Base Data")] 
        [SerializeField] private string m_enemyName;
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Sprite m_enemySprite;
        [SerializeField] private GameObject m_enemyPrefab;
        [Header("Weapon")] 
        [SerializeField] private float m_minBodyDamage;
        [SerializeField] private float m_maxBodyDamage;
        [SerializeField] private float m_delayBetweenShot;
        [Header("Projectile")]
        [SerializeField] private GameObject m_weaponPrefab;
        [SerializeField] private GameObject m_projectilePrefab;
        [SerializeField] private ProjectileType m_projectileType;
        [SerializeField] private float m_projectileSpeed;
        [SerializeField] private float m_projectileRange;
        [SerializeField] private float m_minProjectileDamage;
        [SerializeField] private float m_maxProjectileDamage;

        public string EnemyName => m_enemyName;
        public float MaxHealth => m_maxHealth;
        public float MoveSpeed => m_moveSpeed;
        public Sprite Sprite => m_enemySprite;

        public float MinBodyDamage => m_minBodyDamage;
        public float MaxBodyDamage => m_maxBodyDamage;
        public float DelayBetweenShot => m_delayBetweenShot;
        
        public GameObject WeaponPrefab => m_weaponPrefab;
        public GameObject ProjectilePrefab => m_projectilePrefab;
        public ProjectileType ProjectileType => m_projectileType;
        public float ProjectileSpeed => m_projectileSpeed;
        public float ProjectileRange => m_projectileRange;
        public float MinProjectileDamage => m_minProjectileDamage;
        public float MaxProjectileDamage => m_maxProjectileDamage;

        #if UNITY_EDITOR
        public void ApplyData()
        {
            ApplyHealth();
            ApplyMoveSpeed();
            ApplyBodyDamage();
            ApplyWeaponStats();
            ApplyProjectileStats();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void ApplyHealth()
        {
            if (m_enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab is not found");
                return;
            }
            
            var enemyHealth = m_enemyPrefab.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
            {
                Debug.LogError("Enemy Health is not found");
                return;
            }

            enemyHealth.ApplyDataForHealth(m_maxHealth);
            PrefabUtility.SavePrefabAsset(m_enemyPrefab);
            Debug.Log($"<color=green>Applied health value to enemy prefab {m_enemyPrefab.name}</color>");
        }

        private void ApplyMoveSpeed()
        {
            if (m_enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab is not found");
                return;
            }
            
            var enemyMovement = m_enemyPrefab.GetComponent<EnemyMovement>();

            if (enemyMovement == null)
            {
                Debug.LogError("Enemy Movement is not found");
                return;
            }
            enemyMovement.ApplyMovementData(m_moveSpeed);
            PrefabUtility.SavePrefabAsset(m_enemyPrefab);
            Debug.Log($"<color=green>Applied movespeed value to enemy prefab {m_enemyPrefab.name}</color>");
        }

        private void ApplyBodyDamage()
        {
            if (m_enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab is not found");
                return;
            }
            var damageHandler = m_enemyPrefab.GetComponent<DamageHandler>();
            if (damageHandler == null)
            {
                Debug.LogError("Damage Handler not found");
                return;
            }
            damageHandler.Initialize(m_minBodyDamage, m_maxBodyDamage);
            PrefabUtility.SavePrefabAsset(m_enemyPrefab);
            Debug.Log($"<color=green>Applied body damage value to enemy prefab {m_enemyPrefab.name}</color>");
        }

        private void ApplyWeaponStats()
        {
            if (m_weaponPrefab == null)
            {
                Debug.LogError("Weapon Prefab is not found");
                return;
            }
            
            var weapon = m_weaponPrefab.GetComponent<Weapon>();
            weapon.ApplyData(m_delayBetweenShot);
            PrefabUtility.SavePrefabAsset(m_weaponPrefab);
            Debug.Log($"<color=green>Applied weapon value to enemy prefab {m_enemyPrefab.name}</color>");
        }

        private void ApplyProjectileStats()
        {
            if (m_projectilePrefab == null)
            {
                Debug.LogError("Projectile Prefab is not found");
                return;
            }
            
            
            var projectile = m_projectilePrefab.GetComponent<Projectile>();
            if (projectile == null)
            {
                Debug.LogError("Projectile is not found");
                return;
            }
            
            projectile.ApplyData(m_projectileType, m_projectileSpeed,m_projectileRange,m_minProjectileDamage,m_maxProjectileDamage);
            PrefabUtility.SavePrefabAsset(m_projectilePrefab);
            Debug.Log($"<color=green>Applied projectile value to enemy prefab {m_enemyPrefab.name}</color>");
        }
        
        #endif
    }
}

