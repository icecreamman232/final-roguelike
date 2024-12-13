using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class Key : Pickable
    {
        [SerializeField] private IntEvent m_keyPickedEvent;
        [SerializeField] private int m_keyValue;
        
        protected override void Picked()
        {
            m_keyPickedEvent?.Raise(m_keyValue);
            base.Picked();
        }
    }
}
