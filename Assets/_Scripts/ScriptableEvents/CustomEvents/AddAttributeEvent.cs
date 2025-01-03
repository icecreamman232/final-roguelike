using System;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Add Attribute Event")]
    public class AddAttributeEvent : ScriptableObject
    {
        protected Action<(AttributeTier rate, AttributeType type)> m_listeners;
        
        public void AddListener(Action<(AttributeTier rate, AttributeType type)> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<(AttributeTier rate, AttributeType type)> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise((AttributeTier rate, AttributeType type) reward)
        {
            m_listeners?.Invoke(reward);
        }
    }
}