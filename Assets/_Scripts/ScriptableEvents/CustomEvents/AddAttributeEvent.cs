using System;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Add Attribute Event")]
    public class AddAttributeEvent : ScriptableObject
    {
        protected Action<(AttributeTier tier, AttributeType type)> m_listeners;
        
        public void AddListener(Action<(AttributeTier tier, AttributeType type)> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<(AttributeTier tier, AttributeType type)> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise((AttributeTier tier, AttributeType type) reward)
        {
            m_listeners?.Invoke(reward);
        }
    }
}