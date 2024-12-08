using System;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_healthBar;

        private void OnEnable()
        {
            m_canvasGroup.alpha = 0;
        }

        public void UpdateHealthBar(float value)
        {
            m_healthBar.fillAmount = value;
            m_canvasGroup.alpha = value > 0 ? 1 : 0;
        }
    }  
}

