using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Change Room Event")]
    public class ChangeRoomEvent : ScriptableObject
    {
        protected Action<int,int> m_listeners;
        
        public void AddListener(Action<int,int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<int,int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(int areaIndex, int roomIndex)
        {
            m_listeners?.Invoke(areaIndex, roomIndex);
        }
    }
}