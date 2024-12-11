using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class ModifierProcessor : MonoBehaviour
    {
        [SerializeField] protected bool m_isProcessing;

        public virtual void StartModifier()
        {
            
        }

        public virtual void StopModifier()
        {
            
        }
        
        protected virtual void Update()
        {
            if (!m_isProcessing) return;
        }
    }
}

