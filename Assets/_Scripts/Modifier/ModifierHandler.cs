using System;
using System.Collections.Generic;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class ModifierHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private PlayerHealth m_playerHealth;
        [SerializeField] private List<MovementModifierProcessor> m_movementModifierProcessors;
        [SerializeField] private List<HealthModifierProcessor> m_healthModifierProcessors;
        
        public void RegisterModifier(Modifier modifier)
        {
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    var movementProcessor = this.gameObject.AddComponent<MovementModifierProcessor>();
                    movementProcessor.Initialize(this, m_playerMovement,(MovementModifier)modifier);
                    movementProcessor.StartModifier();
                    m_movementModifierProcessors.Add(movementProcessor);
                    Debug.Log($"<color=orange>Registered Modifier: {modifier.ModifierType}</color>");
                    break;
                case ModifierType.HEALTH:
                    var healthProcessor = this.gameObject.AddComponent<HealthModifierProcessor>();
                    healthProcessor.Initialize(this, m_playerHealth,(HealthModifier)modifier);
                    healthProcessor.StartModifier();
                    m_healthModifierProcessors.Add(healthProcessor);
                    Debug.Log($"<color=orange>Registered Modifier: {modifier.ModifierType}</color>");
                    break;
                case ModifierType.DAMAGE:
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

        public void UnregisterModifier(Modifier info)
        {
            
        }
    }
}

