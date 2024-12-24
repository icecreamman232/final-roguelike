using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Vector2Event m_directionInputEvent;
        [SerializeField] private BoolEvent m_playerFreezeEvent;
        private PlayerInputAction m_playerInputAction;

        private void Awake()
        {
            m_playerFreezeEvent.AddListener(OnPlayerFreeze);
            m_playerInputAction = new PlayerInputAction();
            m_playerInputAction.Enable();
            m_playerInputAction.Player.WASD.ReadValue<Vector2>();
        }
        
        private void Update()
        {
            if (!m_playerInputAction.Player.enabled) return;
            
            m_directionInputEvent.Raise(m_playerInputAction.Player.WASD.ReadValue<Vector2>());
        }
        
        private void OnPlayerFreeze(bool isFrozen)
        {
            if (isFrozen)
            {
                m_playerInputAction.Disable();
            }
            else
            {
                m_playerInputAction.Enable();
            }
        }

    }
}

