using System.Collections;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.StatusEffects
{
    public class AuraHandler : MonoBehaviour
    {
        [SerializeField] private float m_intialRadius;
        [SerializeField] private float m_currentRadius;
        [SerializeField] private float m_interval;
        [SerializeField] private StatusEffectData m_statusEffectData;
        [SerializeField] private CircleCollider2D m_circleCollider2D;
        [SerializeField] private DrawCircleUseLineRenderer m_auraCircleVisual;

        private readonly float C_INCREASE_RADIUS_DURATION = 0.65f;
        private bool m_isCoroutineRunning;
        private bool m_hasTriggered;
        
        private void Start()
        {
            m_currentRadius = m_intialRadius;
            m_circleCollider2D.radius = m_currentRadius;
            m_auraCircleVisual.SetCircleRadius(m_currentRadius);
        }

        [ContextMenu("Test")]
        private void Test()
        {
            IncreaseRadius(2);
        }

        public void IncreaseRadius(float add)
        {
            StartCoroutine(OnIncreaseRadius(add));
        }

        private IEnumerator OnIncreaseRadius(float addRadius)
        {
            if (m_isCoroutineRunning)
            {
                yield break;
            }
            m_isCoroutineRunning = true;
            var timer = 0f;
            var originalValue = m_currentRadius;
            var targetValue = m_currentRadius + addRadius;
            while (timer < C_INCREASE_RADIUS_DURATION)
            {
                timer += Time.deltaTime;
                m_currentRadius = Mathf.Lerp(originalValue,targetValue, MathHelpers.Remap(timer,0,C_INCREASE_RADIUS_DURATION,0,1));
                m_circleCollider2D.radius = m_currentRadius;
                m_auraCircleVisual.SetCircleRadius(m_currentRadius);
                yield return null;
            }

            m_isCoroutineRunning = false;
        }

        private IEnumerator DelayAfterTrigger()
        {
            m_hasTriggered = true;
            yield return new WaitForSeconds(m_interval);
            m_hasTriggered = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (m_hasTriggered) return;
            if (other.CompareTag("Player"))
            {
                var statusFXHandler = ServiceLocator.GetService<PlayerStatusEffectHandler>();
                statusFXHandler.AddStatus(m_statusEffectData,this.gameObject);
                StartCoroutine(DelayAfterTrigger());
            }
            else 
            {
                
            }
        }
    }
}

