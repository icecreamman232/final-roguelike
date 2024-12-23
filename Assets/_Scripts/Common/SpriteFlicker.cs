using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class SpriteFlicker : MonoBehaviour
    {
        [SerializeField] private Color m_flickerColor;
        
        public void FlickerSprite(SpriteRenderer spriteRenderer,float duration, float frequency)
        {
            StartCoroutine(OnFlicking(spriteRenderer, duration, frequency));
        }

        private IEnumerator OnFlicking(SpriteRenderer spriteRenderer, float duration, float frequency)
        {
            var timeStop = duration + Time.time;
            while (Time.time < timeStop)
            {
                spriteRenderer.color = m_flickerColor;
                yield return new WaitForSeconds(frequency);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(frequency);
            }
            spriteRenderer.color = Color.white;
        }
    }
}

