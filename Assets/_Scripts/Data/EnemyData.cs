using SGGames.Scripts.Weapons;
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
    }
}

