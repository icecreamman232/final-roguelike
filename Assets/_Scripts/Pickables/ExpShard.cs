using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ExpShard : Pickable
    {
        [SerializeField] private IntEvent m_expPickedEvent;
        [SerializeField] private int m_expValue;

        protected override void Picked()
        {
            m_expPickedEvent?.Raise(m_expValue);
            base.Picked();
        }
    }
}

