using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Action Event")]
    public class ActionEvent : ScriptableObject
    {
        protected Action m_listeners;
        
        public void AddListener(Action addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise()
        {
            m_listeners?.Invoke();
        }
    } 
}

