using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class PlayerStatusSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPooler m_statusTextPooler;
        [SerializeField] private Color m_normalColor;
        [SerializeField] private Color m_healingColor;
        [SerializeField] private Color m_immortalColor;
        
        private PlayerHealth m_PlayerHealth;

        private void Start()
        {
            m_PlayerHealth = GetComponentInParent<PlayerHealth>();
            m_PlayerHealth.OnHit += OnPlayerGetHit;
            m_PlayerHealth.OnHealing += OnPlayerHealing;
        }

        private void OnDestroy()
        {
            m_PlayerHealth.OnHit -= OnPlayerGetHit;
            m_PlayerHealth.OnHealing -= OnPlayerHealing;
        }
        
        private void OnPlayerHealing(float amount)
        {
            var textObj = m_statusTextPooler.GetPooledGameObject();
            var textMesh = textObj.GetComponentInChildren<TextMeshPro>();
            textMesh.color = m_healingColor;
            textMesh.text = $"+{amount}";
        }

        private void OnPlayerGetHit(bool isDodge, bool isImmortal)
        {
            if (isImmortal)
            {
                var textObj = m_statusTextPooler.GetPooledGameObject();
                var textMesh = textObj.GetComponentInChildren<TextMeshPro>();
                textMesh.color = m_immortalColor;
                textMesh.text = "Immunity";
            }
            else if (isDodge)
            {
                var textObj = m_statusTextPooler.GetPooledGameObject();
                var textMesh = textObj.GetComponentInChildren<TextMeshPro>();
                textMesh.color = m_normalColor;
                textMesh.text = "Dodge";
            }
        }
    }
}

