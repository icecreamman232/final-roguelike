using System.Collections;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_healthBar;
        [SerializeField] private GenericBossEvent m_genericBossEvent;
        [SerializeField] private BossHealthUpdateEvent m_bossHealthUpdateEvent;

        private void Awake()
        {
            m_genericBossEvent.AddListener(OnGenericBossEvent);
            m_bossHealthUpdateEvent.AddListener(OnBossHealthUpdate);
        }

        private void OnDestroy()
        {
            m_genericBossEvent.RemoveListener(OnGenericBossEvent);
            m_bossHealthUpdateEvent.RemoveListener(OnBossHealthUpdate);
        }
        
        private void Show()
        {
            StartCoroutine(OnShowingBarAnim());
        }
        
        private IEnumerator OnShowingBarAnim()
        {
            var duration = 1.5f;
            m_healthBar.fillAmount = 0;
            m_canvasGroup.alpha = 1;
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                m_healthBar.fillAmount = Mathf.Lerp(0f, 1f, 1 - duration/1.5f);
                yield return null;
            }

            m_canvasGroup.alpha = 1;
            m_genericBossEvent?.Raise(GenericBossEventType.START_FIGHT);
        }
        
        private void OnBossHealthUpdate(float current, float max)
        {
            m_healthBar.fillAmount = MathHelpers.Remap(current, 0, max, 0, 1);
        }
        
        private void OnGenericBossEvent(GenericBossEventType eventType)
        {
            switch (eventType)
            {
                case GenericBossEventType.SHOW_HEALTH_BAR:
                    Show();
                    break;
                case GenericBossEventType.START_FIGHT:
                    break;
                case GenericBossEventType.END_FIGHT:
                    break;
  
            }
        }
    }
}
