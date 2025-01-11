using System.Collections;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class SpiderWebController : MonoBehaviour
    {
        [SerializeField] private MovementModifier m_reduceMSModifier;
        [SerializeField] private float m_delayBeforeTrigger;
        [SerializeField] private float m_destroyAfterDuration;
        private bool m_canTrigger;

        private void OnEnable()
        {
            StartCoroutine(DelayBeforeTrigger());
        }

        private IEnumerator DelayBeforeTrigger()
        {
            m_canTrigger = false;
            yield return new WaitForSeconds(m_delayBeforeTrigger);
            m_canTrigger = true;
            StartCoroutine(DestroyAfterDuration());
        }

        private IEnumerator DestroyAfterDuration()
        {
            yield return new WaitForSeconds(m_destroyAfterDuration);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && m_canTrigger)
            {
                var handler  = other.gameObject.GetComponentInChildren<PlayerModifierHandler>();
                handler.RegisterModifier(m_reduceMSModifier);
                Destroy(this.gameObject);
            }
        }
    }
}

