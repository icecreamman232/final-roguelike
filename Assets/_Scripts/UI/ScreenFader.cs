using System.Collections;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ScreenFader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private BoolEvent m_fadeScreenEvent;

        private bool m_isFading = false;
        public static float FadeDuration = 0.5f;
        
        private void Start()
        {
            m_canvasGroup.alpha = 1;
            m_fadeScreenEvent.AddListener(OnFadeScreen);
        }

        private void OnDestroy()
        {
            m_fadeScreenEvent.RemoveListener(OnFadeScreen);
        }

        private void OnFadeScreen(bool isFadeOut)
        {
            if (m_isFading) return;
            
            if (isFadeOut)
            {
                StartCoroutine(OnFadingOut());
            }
            else
            {
                StartCoroutine(OnFadingIn());
            }
        }

        private IEnumerator OnFadingIn()
        {
            m_isFading = true;
            var timer = FadeDuration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                m_canvasGroup.alpha = MathHelpers.Remap(timer,0,FadeDuration,0,1);
                yield return null;
            }

            m_canvasGroup.alpha = 0;
            m_isFading = false;
        }

        private IEnumerator OnFadingOut()
        {
            m_isFading = true;
            var timer = 0f;
            while (timer < FadeDuration)
            {
                timer += Time.deltaTime;
                m_canvasGroup.alpha = MathHelpers.Remap(timer,0,FadeDuration,0,1);
                yield return null;
            }
            
            m_canvasGroup.alpha = 1; 
            m_isFading = false;
        }
    }
}


