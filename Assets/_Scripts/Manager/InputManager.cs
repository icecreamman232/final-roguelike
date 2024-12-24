using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Vector2Event m_directionInputEvent;
        [SerializeField] private BoolEvent m_freezeInputEvent;
        [SerializeField] private InputContextEvent m_inventoryButtonPressedEvent;
        [SerializeField] private InputContextEvent m_characterInfoButtonPressedEvent;
        [SerializeField] private InputContextEvent m_interactButtonPressedEvent;
        [SerializeField] private InputContextEvent m_defenseButtonPressedEvent;
        private PlayerInputAction m_playerInputAction;

        private void Awake()
        {
            m_freezeInputEvent.AddListener(OnFreezeInput);
            m_playerInputAction = new PlayerInputAction();
            m_playerInputAction.Enable();
            m_playerInputAction.Player.WASD.ReadValue<Vector2>();
            m_playerInputAction.Player.Inventory.performed += OnInventoryButtonPressed;
            m_playerInputAction.Player.CharacterInfo.performed += OnCharacterInfoButtonPressed;
            m_playerInputAction.Player.Interact.performed += OnInteractButtonPressed;
            m_playerInputAction.Player.DefenseAbility.performed += OnDefenseButtonPressed;
        }
        
        private void Update()
        {
            if (!m_playerInputAction.Player.enabled) return;
            
            m_directionInputEvent.Raise(m_playerInputAction.Player.WASD.ReadValue<Vector2>());
        }
        
        private void OnFreezeInput(bool isFrozen)
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
        
        private void OnInventoryButtonPressed(InputAction.CallbackContext context)
        {
            m_inventoryButtonPressedEvent.Raise(context);
        }
        
        private void OnCharacterInfoButtonPressed(InputAction.CallbackContext context)
        {
            m_characterInfoButtonPressedEvent.Raise(context);
        }
        
        private void OnInteractButtonPressed(InputAction.CallbackContext context)
        {
            m_interactButtonPressedEvent.Raise(context);
        }
        
        private void OnDefenseButtonPressed(InputAction.CallbackContext context)
        {
            m_defenseButtonPressedEvent.Raise(context);
        }
        
        private void OnDestroy()
        {
            m_playerInputAction.Player.Interact.performed -= OnInventoryButtonPressed;
            m_playerInputAction.Player.CharacterInfo.performed -= OnCharacterInfoButtonPressed;
            m_playerInputAction.Player.Interact.performed -= OnInteractButtonPressed;
            m_playerInputAction.Player.DefenseAbility.performed -= OnDefenseButtonPressed;
            m_freezeInputEvent.RemoveListener(OnFreezeInput);
        }
    }
}

