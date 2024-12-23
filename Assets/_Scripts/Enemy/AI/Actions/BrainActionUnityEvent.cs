using UnityEngine;
using UnityEngine.Events;

namespace SGGames.Scripts.Enemies
{
    public class BrainActionUnityEvent : BrainAction
    {
        [SerializeField] private UnityEvent m_triggerEvent;
        public override void DoAction()
        {
            m_triggerEvent?.Invoke();
        }
    }
}

