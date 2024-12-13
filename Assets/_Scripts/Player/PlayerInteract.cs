using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    public enum InteractType
    {
        None,
        Chest,
    }
    
    public interface IInteractable
    {
        public void Interact();
    }
    
    public class PlayerInteract : PlayerBehavior
    {
        [SerializeField] private InteractType m_currentInteractType;
        
        private IInteractable m_interactObject;
        private PlayerInputAction m_playerInput;

        protected override void Start()
        {
            base.Start();
            m_playerInput = new PlayerInputAction();
            m_playerInput.Enable();
            m_playerInput.Player.Interact.performed += InteractOnPerformed;
        }

        private void InteractOnPerformed(InputAction.CallbackContext context)
        {
            if (m_currentInteractType == InteractType.None) return;
            
            m_interactObject.Interact();
        }

        public void AssignInteraction(InteractType type, IInteractable interactableObject)
        {
            m_currentInteractType = type;
            m_interactObject = interactableObject;
        }
    }
}

