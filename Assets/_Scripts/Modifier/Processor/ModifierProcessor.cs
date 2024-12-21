using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ModifierProcessor : MonoBehaviour
    {
        [SerializeField] protected string m_id;
        [SerializeField] protected Modifier m_modifier;
        [SerializeField] protected bool m_isProcessing;

        protected float m_timer;
        protected ModifierHandler m_handler;
        
        public string Id => m_id;
        public Modifier Modifier => m_modifier;
        
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

