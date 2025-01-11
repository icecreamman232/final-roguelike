using System;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class EnemyMovementModifierProcessor : EnemyModifierProcessor
    {
        public override void StartModifier()
        {
            base.StartModifier();
            var moveSpeedModifier = (MovementModifier)m_modifier;
            switch (moveSpeedModifier.MovementModifierType)
            {
                case MovementModifierType.ModifyMovespeedForDuration:
                    m_handler.EnemyMovement.ModifySpeed(moveSpeedModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case MovementModifierType.ModifyMovespeed:
                    m_handler.EnemyMovement.ModifySpeed(moveSpeedModifier.ModifierValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            m_modifier.IsRunning = true;
        }

        public override void StopModifier()
        {
            var moveSpeedModifier = (MovementModifier)m_modifier;

            switch (moveSpeedModifier.MovementModifierType)
            {
                case MovementModifierType.ModifyMovespeedForDuration:
                case MovementModifierType.ModifyMovespeed:
                    m_handler.EnemyMovement.ModifySpeed(-moveSpeedModifier.ModifierValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
