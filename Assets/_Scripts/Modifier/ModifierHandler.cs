using System;
using System.Collections.Generic;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ModifierHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private PlayerHealth m_playerHealth;
        [SerializeField] private PlayerDamageComputer m_damageComputer;
        [SerializeField] private List<MovementModifierProcessor> m_movementModifierProcessors;
        [SerializeField] private List<HealthModifierProcessor> m_healthModifierProcessors;
        [SerializeField] private List<DamageModifierProcessor> m_damageModifierProcessors;
        
        public void RegisterModifier(Modifier modifier)
        {
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    var movementProcessor = this.gameObject.AddComponent<MovementModifierProcessor>();
                    movementProcessor.Initialize(this, m_playerMovement,(MovementModifier)modifier);
                    movementProcessor.StartModifier();
                    m_movementModifierProcessors.Add(movementProcessor);
                    break;
                case ModifierType.HEALTH:
                    var healthProcessor = this.gameObject.AddComponent<HealthModifierProcessor>();
                    healthProcessor.Initialize(this, m_playerHealth,(HealthModifier)modifier);
                    healthProcessor.StartModifier();
                    m_healthModifierProcessors.Add(healthProcessor);
                    break;
                case ModifierType.DAMAGE:
                    var damageProcessor = this.gameObject.AddComponent<DamageModifierProcessor>();
                    damageProcessor.Initialize(this, m_damageComputer,(DamageModifier)modifier);
                    damageProcessor.StartModifier();
                    m_damageModifierProcessors.Add(damageProcessor);
                    break;
            }
        }

        public void RemoveMovementModifierProcessor(MovementModifierProcessor processor)
        {
            if (!m_movementModifierProcessors.Contains(processor)) return;
            m_movementModifierProcessors.Remove(processor);
            DestroyImmediate(processor);
        }
        
        public void RemoveHealthModifierProcessor(HealthModifierProcessor processor)
        {
            if (!m_healthModifierProcessors.Contains(processor)) return;
            m_healthModifierProcessors.Remove(processor);
            DestroyImmediate(processor);
        }
        
        public void RemoveDamageModifierProcessor(DamageModifierProcessor processor)
        {
            if (!m_damageModifierProcessors.Contains(processor)) return;
            m_damageModifierProcessors.Remove(processor);
            DestroyImmediate(processor);
        }

        public void UnregisterModifier(Modifier info)
        {
            
        }
    }
}

