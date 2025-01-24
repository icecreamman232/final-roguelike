using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class AnimationParameter : MonoBehaviour
    {
        [SerializeField] private string m_parameterName;
        [SerializeField] private float m_duration;
        [SerializeField] private Animator m_animator;
        
        public float Duration => m_duration;

        private int m_parameterHash;
        
        private void Start()
        {
            if (m_animator == null)
            {
                m_animator = GetComponent<Animator>();
                if (m_animator == null)
                {
                    Debug.LogError($"Animator component is missing on {this.gameObject.name}");
                    return;
                }
            }
            
            m_parameterHash = Animator.StringToHash(m_parameterName);
        }

        public void SetTrigger()
        {
            m_animator.SetTrigger(m_parameterHash);
        }

        public void SetBool(bool value)
        {
            m_animator.SetBool(m_parameterHash, value);
        }

        public void SetFloat(float value)
        {
            m_animator.SetFloat(m_parameterHash, value);
        }

        public void SetInt(int value)
        {
            m_animator.SetInteger(m_parameterHash, value);
        }
    }
}
