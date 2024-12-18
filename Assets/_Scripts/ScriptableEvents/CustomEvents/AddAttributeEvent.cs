using System;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Add Attribute Event")]
    public class AddAttributeEvent : ScriptableObject
    {
        protected Action<(UpgradeAttributeRate rate, AttributeType type)> m_listeners;
        
        public void AddListener(Action<(UpgradeAttributeRate rate, AttributeType type)> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<(UpgradeAttributeRate rate, AttributeType type)> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise((UpgradeAttributeRate rate, AttributeType type) reward)
        {
            m_listeners?.Invoke(reward);
        }
    }
}