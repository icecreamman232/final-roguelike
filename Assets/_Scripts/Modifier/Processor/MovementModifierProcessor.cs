using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class MovementModifierProcessor : ModifierProcessor
    {
        [SerializeField] private PlayerMovement m_playerMovement;
        
        public override void Initialize(string id, ModifierHandler handler, Modifier modifier)
        {
            base.Initialize(id,handler,modifier);
            m_playerMovement = handler.PlayerMovement;
        }
        
        public override void StartModifier()
        {
            base.StartModifier();
            switch (((MovementModifier)m_modifier).MovementModifierType)
            {
                case MovementModifierType.ReduceMSForDuration:
                    m_playerMovement.ModifySpeed(-((MovementModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.IncreaseMSForDuration:
                    m_playerMovement.ModifySpeed(((MovementModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.OverrideSpeedForDuration:
                    m_playerMovement.OverrideSpeed(((MovementModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.ReduceMS:
                    m_playerMovement.ModifySpeed(-((MovementModifier)m_modifier).ModifierValue);
                    break;
                case MovementModifierType.IncreaseMS:
                    m_playerMovement.ModifySpeed(((MovementModifier)m_modifier).ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeed:
                    m_playerMovement.OverrideSpeed(((MovementModifier)m_modifier).ModifierValue);
                    break;
            }

            m_modifier.IsRunning = true;
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((MovementModifier)m_modifier).MovementModifierType} " +
                      $"- Value:{((MovementModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((MovementModifier)m_modifier).MovementModifierType} " +
                      $"- Value:{((MovementModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            switch (((MovementModifier)m_modifier).MovementModifierType)
            {
                case MovementModifierType.ReduceMSForDuration:
                    m_playerMovement.ModifySpeed(((MovementModifier)m_modifier).ModifierValue);
                    break;
                case MovementModifierType.IncreaseMSForDuration:
                    m_playerMovement.ModifySpeed(-((MovementModifier)m_modifier).ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeedForDuration:
                    m_playerMovement.ResetSpeed();
                    break;
                case MovementModifierType.ReduceMS:
                    m_playerMovement.ModifySpeed(((MovementModifier)m_modifier).ModifierValue);
                    break;
                case MovementModifierType.IncreaseMS:
                    m_playerMovement.ModifySpeed(-((MovementModifier)m_modifier).ModifierValue);
                    break;
                case MovementModifierType.OverrideSpeed:
                    break;
            }

            base.StopModifier();
        }

        protected override void Update()
        {
            if (!m_isProcessing) return;
            
            m_timer += Time.deltaTime;
            if (m_timer >= m_modifier.Duration)
            {
                m_timer = 0;
                StopModifier();
            }
        }
    }
}

