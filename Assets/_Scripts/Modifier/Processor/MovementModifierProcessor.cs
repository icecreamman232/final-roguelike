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
            var moveSpeedModifier = (MovementModifier)m_modifier;
            
            switch (moveSpeedModifier.MovementModifierType)
            {
                case MovementModifierType.ModifyMovespeedForDuration:
                    m_playerMovement.ModifySpeed(moveSpeedModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.ModifyMovespeed:
                    m_playerMovement.ModifySpeed(moveSpeedModifier.ModifierValue);
                    break;
            }

            m_modifier.IsRunning = true;
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{moveSpeedModifier.MovementModifierType} " +
                      $"- Value:{moveSpeedModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            var moveSpeedModifier = (MovementModifier)m_modifier;
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{moveSpeedModifier.MovementModifierType} " +
                      $"- Value:{moveSpeedModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            switch (moveSpeedModifier.MovementModifierType)
            {
                case MovementModifierType.ModifyMovespeedForDuration:
                    m_playerMovement.ModifySpeed(-moveSpeedModifier.ModifierValue);
                    break;
                case MovementModifierType.ModifyMovespeed:
                    m_playerMovement.ModifySpeed(-moveSpeedModifier.ModifierValue);
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

