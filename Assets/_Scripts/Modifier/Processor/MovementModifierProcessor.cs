using SGGames.Scripts.Player;
using UnityEngine;


namespace SGGames.Scripts.Modifier
{
    public class MovementModifierProcessor : ModifierProcessor, IOverTimeModifierProcessor
    {
        [SerializeField] private MovementModifier m_modifier;
        [SerializeField] private PlayerMovement m_playerMovement;
        private float m_timer;
        private ModifierHandler m_handler;
        
        public void Initialize(ModifierHandler handler, PlayerMovement playerMovement, MovementModifier modifier)
        {
            m_handler = handler;
            m_playerMovement = playerMovement;
            m_modifier = modifier;
        }
        
        public void StartModifier()
        {
            switch (m_modifier.MovementModifierType)
            {
                case MovementModifierType.ReduceMSForDuration:
                    m_playerMovement.ModifySpeed(-m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.IncreaseMSForDuration:
                    m_playerMovement.ModifySpeed(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.OverrideSpeedForDuration:
                    m_playerMovement.OverrideSpeed(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.ReduceMS:
                    m_playerMovement.ModifySpeed(-m_modifier.ModifierValue);
                    break;
                case MovementModifierType.IncreaseMS:
                    m_playerMovement.ModifySpeed(m_modifier.ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeed:
                    m_playerMovement.OverrideSpeed(m_modifier.ModifierValue);
                    break;
            }

            
            Debug.Log("<color=orange>Start Movement Modifier </color>");
        }
    
        public void StopModifier()
        {
            Debug.Log("<color=orange>Stop Movement Modifier </color>");
            switch (m_modifier.MovementModifierType)
            {
                case MovementModifierType.ReduceMSForDuration:
                    m_playerMovement.ModifySpeed(m_modifier.ModifierValue);
                    break;
                case MovementModifierType.IncreaseMSForDuration:
                    m_playerMovement.ModifySpeed(-m_modifier.ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeedForDuration:
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
            if (m_timer >= m_modifier.Duration)
            {
                StopModifier();
            }
        }
    }
}

