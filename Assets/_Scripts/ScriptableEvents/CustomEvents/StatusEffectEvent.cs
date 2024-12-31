using System;
using SGGames.Scripts.StatusEffects;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "StatusEffectEvent", menuName = "SGGames/Event/Status Effect Events")]
    public class StatusEffectEvent : ScriptableObject
    {
        protected Action<StatusEffectType, int> m_listeners;
        
        public void AddListener(Action<StatusEffectType, int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<StatusEffectType, int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(StatusEffectType effectType, int stackNumber)
        {
            m_listeners?.Invoke(effectType, stackNumber);
        }
        
    }
}

