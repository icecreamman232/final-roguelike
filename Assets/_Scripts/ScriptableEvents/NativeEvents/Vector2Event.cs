using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Vector2 Event")]
    public class Vector2Event : ScriptableObject
    {
        protected Action<Vector2> m_listeners;
    
        public void AddListener(Action<Vector2> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<Vector2> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(Vector2 vector)
        {
            m_listeners?.Invoke(vector);
        }
    } 
}
