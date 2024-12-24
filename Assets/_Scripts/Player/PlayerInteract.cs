using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Rooms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    public interface IInteractable
    {
        public void Interact();
    }
    
    public class PlayerInteract : PlayerBehavior
    {
        [SerializeField] private InteractType m_currentInteractType;
        [SerializeField] private InputContextEvent m_interactButtonPressedEvent;
        
        private IInteractable m_interactObject;

        protected override void Start()
        {
            base.Start();
            m_interactButtonPressedEvent.AddListener(InteractOnPerformed);
        }

        private void InteractOnPerformed(InputAction.CallbackContext context)
        {
            if (m_currentInteractType == InteractType.None) return;

            //The Legendary chest requires key to open
            if (m_currentInteractType == InteractType.Chest 
                && ((Chest)m_interactObject).ChestType == ChestType.Legendary)
            {
                if (!CurrencyManager.Instance.HasKey) return;
            }
            
            m_interactObject.Interact();
        }

        public void AssignInteraction(InteractType type, IInteractable interactableObject)
        {
            m_currentInteractType = type;
            m_interactObject = interactableObject;
        }

        private void OnDestroy()
        {
            m_interactButtonPressedEvent.RemoveListener(InteractOnPerformed);
        }
    }
}

