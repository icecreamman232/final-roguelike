using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class MovementModifierProcessor : ModifierProcessor, IOverTimeModifierProcessor
    {
        [SerializeField] private MovementModifierInfo m_info;
        [SerializeField] private PlayerMovement m_playerMovement;
        private float m_timer;
        private ModifierHandler m_handler;
        
        public void Initialize(ModifierHandler handler, PlayerMovement playerMovement, MovementModifierInfo info)
        {
            m_handler = handler;
            m_playerMovement = playerMovement;
            m_info = info;
        }
        
        public void StartModifier()
        {
            switch (m_info.MovementModifierType)
            {
                case MovementModifierType.ReduceMS:
                    m_playerMovement.ModifySpeed(-m_info.ModifierValue);
                    break;
                case MovementModifierType.IncreaseMS:
                    m_playerMovement.ModifySpeed(m_info.ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeed:
                    m_playerMovement.OverrideSpeed(m_info.ModifierValue);
                    break;
            }

            m_isProcessing = true;
            Debug.Log("<color=orange>Start Movement Modifier </color>");
        }
    
        public void StopModifier()
        {
            Debug.Log("<color=orange>Stop Movement Modifier </color>");
            switch (m_info.MovementModifierType)
            {
                case MovementModifierType.ReduceMS:
                    m_playerMovement.ModifySpeed(m_info.ModifierValue);
                    break;
                case MovementModifierType.IncreaseMS:
                    m_playerMovement.ModifySpeed(-m_info.ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeed:
                    m_playerMovement.ResetSpeed();
                    break;
            }

            m_isProcessing = false;
            m_handler.RemoveMovementModifierProcessor(this);
        }

        protected override void Update()
        {
            if (!m_isProcessing) return;
            
            m_timer += Time.deltaTime;
            if (m_timer >= m_info.Duration)
            {
                StopModifier();
            }
        }
    }
}

