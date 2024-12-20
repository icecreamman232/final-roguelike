using System;
using System.Collections.Generic;
using System.Linq;
using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGGames.Scripts.Modifiers
{
    public class ModifierHandler : MonoBehaviour
    {
        [Header("Player Components")]
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private PlayerHealth m_playerHealth;
        [SerializeField] private PlayerDamageComputer m_damageComputer;
        [Header("Events")] 
        [SerializeField] private GameEvent m_gameEvent;
        [Header("Processors")]
        [SerializeField] private List<MovementModifierProcessor> m_movementModifierProcessors;
        [SerializeField] private List<HealthModifierProcessor> m_healthModifierProcessors;
        [SerializeField] private List<DamageModifierProcessor> m_damageModifierProcessors;
        [SerializeField] private List<TriggerAfterEventModifierProcessor> m_triggerAfterEventModifierProcessors;
        [SerializeField] private List<ArmorModifierProcessor> m_armorModifierProcessors;

        private void Start()
        {
            m_gameEvent.AddListener(OnReceiveGameEvent);
        }

        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnReceiveGameEvent);
        }

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
                case ModifierType.TRIGGER_AFTER_GAME_EVENT:
                    var triggerProcessor = this.gameObject.AddComponent<TriggerAfterEventModifierProcessor>();
                    triggerProcessor.Initialize(this, (TriggerAfterEventModifier)modifier);
                    m_triggerAfterEventModifierProcessors.Add(triggerProcessor);
                    break;
                case ModifierType.ARMOR:
                    var armorProcessor = this.gameObject.AddComponent<ArmorModifierProcessor>();
                    armorProcessor.Initialize(this, (ArmorModifier)modifier,m_playerHealth);
                    armorProcessor.StartModifier();
                    m_armorModifierProcessors.Add(armorProcessor);
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
        
        public void RemoveTriggerAfterEventModifierProcessor(TriggerAfterEventModifierProcessor processor)
        {
            if (!m_triggerAfterEventModifierProcessors.Contains(processor)) return;
            m_triggerAfterEventModifierProcessors.Remove(processor);
            DestroyImmediate(processor);
        }
        
        public void RemoveArmorModifierProcessor(ArmorModifierProcessor processor)
        {
            if (!m_armorModifierProcessors.Contains(processor)) return;
            m_armorModifierProcessors.Remove(processor);
            DestroyImmediate(processor);
        }

        public void UnregisterModifier(Modifier info)
        {
            switch (info.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    var movementProcessor = m_movementModifierProcessors.FirstOrDefault(x => x.Modifier == info);
                    movementProcessor?.StopModifier();
                    break;
                case ModifierType.HEALTH:
                    var healthProcessor = m_healthModifierProcessors.FirstOrDefault(x => x.Modifier == info);
                    healthProcessor?.StopModifier();
                    break;
                case ModifierType.DAMAGE:
                    var damageProcessor = m_damageModifierProcessors.FirstOrDefault(x => x.Modifier == info);
                    damageProcessor?.StopModifier();
                    break;
                case ModifierType.TRIGGER_AFTER_GAME_EVENT:
                    var triggerAfterEventModifierProcessor = m_triggerAfterEventModifierProcessors.FirstOrDefault(x => x.Modifier == info);
                    triggerAfterEventModifierProcessor?.StopModifier();
                    break;
                case ModifierType.ARMOR:
                    var armorProcessor = m_triggerAfterEventModifierProcessors.FirstOrDefault(x => x.Modifier == info);
                    armorProcessor?.StopModifier();
                    break;
            }
        }
        
        private void OnReceiveGameEvent(GameEventType eventType)
        {
            for (int i = 0; i < m_triggerAfterEventModifierProcessors.Count; i++)
            {
                if (m_triggerAfterEventModifierProcessors[i].Modifier.EventTypeToTrigger == eventType)
                {
                    m_triggerAfterEventModifierProcessors[i].StartModifier();
                    if (m_triggerAfterEventModifierProcessors[i].Modifier.TriggerOnce)
                    {
                        RemoveTriggerAfterEventModifierProcessor(m_triggerAfterEventModifierProcessors[i]);
                    }
                }
            }
        }
    }
}

