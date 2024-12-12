using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class Coin : Pickable
    {
        [SerializeField] private IntEvent m_coinPickedEvent;
        [SerializeField] private int m_coinValue;

        protected override void Picked()
        {
            m_coinPickedEvent?.Raise(m_coinValue);
            base.Picked();
        }
    }
}

