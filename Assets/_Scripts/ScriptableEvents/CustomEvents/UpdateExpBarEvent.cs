using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    
    [CreateAssetMenu(menuName = "SGGames/Event/Update Exp Bar Event")]
    public class UpdateExpBarEvent : ScriptableObject
    {
        protected Action<int,int,int> m_listeners;
        
        public void AddListener(Action<int,int,int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<int,int,int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(int current, int max, int level)
        {
            m_listeners?.Invoke(current,max,level);
        }
    } 
}
