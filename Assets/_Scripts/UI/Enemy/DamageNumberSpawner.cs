using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyHealth m_enemyHealth;
        [SerializeField] private ObjectPooler m_damageNumberPooler;
        [SerializeField] private Color m_normalColor;
        [SerializeField] private Color m_criticalColor;
        [SerializeField] private float m_showRadius;

        private readonly float m_criticalHitTextScale = 1.3f;
        
        private void Start()
        {
            m_enemyHealth = GetComponentInParent<EnemyHealth>();
            m_enemyHealth.OnHit += Show;
        }

        private void OnDestroy()
        {
            m_enemyHealth.OnHit -= Show;
        }

        private void Show(float damage, bool isCritical)
        {
            var dmgNumberObj = m_damageNumberPooler.GetPooledGameObject();
            dmgNumberObj.transform.localScale = isCritical ? Vector3.one * m_criticalHitTextScale : Vector3.one;
            dmgNumberObj.transform.position = transform.position + (Vector3)Random.insideUnitCircle * m_showRadius;
            var damageText = dmgNumberObj.GetComponentInChildren<TextMeshPro>();
            damageText.color = isCritical ? m_criticalColor : m_normalColor;
            damageText.text = isCritical ? $"{damage.ToString()} !" :damage.ToString();
        }
    }
}

