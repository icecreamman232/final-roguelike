using System;
using System.Collections;
using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class DisableAfterDuration : MonoBehaviour
    {
        [SerializeField] private GameObject m_target;
        [SerializeField] private float m_durationBeforeDisable;
        
        private void OnEnable()
        {
            StartCoroutine(BeforeDisable());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator BeforeDisable()
        {
            yield return new WaitForSeconds(m_durationBeforeDisable);
            m_target.SetActive(false);
        }
    }
}

