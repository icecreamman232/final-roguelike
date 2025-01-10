using System;
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

        public virtual void Initialize(string id, ModifierHandler modifierHandler, Modifier modifier)
        {
            m_id = id;
            m_handler = modifierHandler;
            m_modifier = modifier;
        }
        
        public virtual void StartModifier()
        {
            
        }

        public virtual void StopModifier()
        {
            PostTrigger();
        }

        protected virtual void PostTrigger()
        {
            m_isProcessing = false;
            m_modifier.IsRunning = false;
            
            if (m_modifier.PostTriggerBehavior == PostTriggerModifierBehavior.SELF_REMOVED)
            {
                m_handler.RemoveProcessor(this);
            }
        }
        
        protected virtual void Update()
        {
            if (!m_isProcessing) return;
        }

        protected virtual bool Compare(ComparisonType type, float toCompare, float tobeCompared)
        {
            switch (type)
            {
                case ComparisonType.Equal:
                    return Mathf.Abs(toCompare - tobeCompared) < 0.1f;
                case ComparisonType.EqualAndGreaterThan:
                    return toCompare >= tobeCompared;
                case ComparisonType.GreaterThan:
                    return toCompare > tobeCompared;
                case ComparisonType.EqualAndLessThan:
                    return toCompare <= tobeCompared;
                case ComparisonType.LessThan:
                    return toCompare < tobeCompared;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}

