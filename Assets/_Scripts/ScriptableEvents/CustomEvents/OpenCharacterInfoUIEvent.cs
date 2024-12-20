using System;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Open Character Info UI Event")]
    public class OpenCharacterInfoUIEvent : ScriptableObject
    {
        protected Action<bool,PlayerAttributeController> m_listeners;
        
        public void AddListener(Action<bool,PlayerAttributeController> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<bool,PlayerAttributeController> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(bool isOpen,PlayerAttributeController playerAttributeController)
        {
            m_listeners?.Invoke(isOpen, playerAttributeController);
        }
    } 
}