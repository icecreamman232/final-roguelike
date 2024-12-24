using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Events
{
    
    [CreateAssetMenu(menuName = "SGGames/Event/Input Context Event")]
    public class InputContextEvent : ScriptableObject
    {
        protected Action<InputAction.CallbackContext> m_listeners;
        
        public void AddListener(Action<InputAction.CallbackContext> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<InputAction.CallbackContext> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(InputAction.CallbackContext context)
        {
            m_listeners?.Invoke(context);
        }
    }
}