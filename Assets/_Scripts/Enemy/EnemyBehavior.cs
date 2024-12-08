using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] protected bool m_isAllow;

        public void ToggleAllow(bool value)
        {
            m_isAllow = value;
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            if (!m_isAllow) return;
        }
    }
}

