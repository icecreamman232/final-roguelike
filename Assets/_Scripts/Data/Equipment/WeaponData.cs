
using SGGames.Scripts.Common;
using SGGames.Scripts.Weapons;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Weapon",fileName = "WeaponData")]
    public class WeaponData : ItemData
    {
        [Header("Weapon")] 
        [SerializeField] private WeaponCategory m_weaponCategory;
        [SerializeField] private float m_minDamage;
        [SerializeField] private float m_maxDamage;
        [SerializeField] private float m_baseDelayBetweenShots;
        [SerializeField] private float m_attackRange;
        [SerializeField] private float m_projectileSpeed;
        [Header("Prefab")]
        [SerializeField] private GameObject m_weaponPrefab;
        [SerializeField] private GameObject m_projectilePrefab;
        
        
        public WeaponCategory WeaponCategory => m_weaponCategory;
        public float MinDamage => m_minDamage;
        public float MaxDamage => m_maxDamage;
        public float BaseDelayBetweenShots => m_baseDelayBetweenShots;
        public float AttackRange => m_attackRange;
        public float ProjectileSpeed => m_projectileSpeed;
        public GameObject WeaponPrefab => m_weaponPrefab;
        public GameObject ProjectilePrefab => m_projectilePrefab;

#if UNITY_EDITOR
        public void ApplyData()
        {
            var weapon = m_weaponPrefab.GetComponent<Weapon>();
            weapon.ApplyData(this);
            
            PrefabUtility.SavePrefabAsset(m_weaponPrefab);
            PrefabUtility.SavePrefabAsset(m_projectilePrefab);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}
