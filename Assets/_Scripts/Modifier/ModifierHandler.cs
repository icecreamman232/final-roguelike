using System.Collections.Generic;
using System.Linq;
using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Player;
using UnityEngine;

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
        private Dictionary<string,ModifierProcessor> m_processorContainer;

        private void Start()
        {
            m_gameEvent.AddListener(OnReceiveGameEvent);
            m_processorContainer = new Dictionary<string, ModifierProcessor>();
        }

        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnReceiveGameEvent);
        }

        public void RegisterModifier(Modifier modifier)
        {
            var uniqueID = RandomController.GetUniqueID();
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    var movementProcessor = this.gameObject.AddComponent<MovementModifierProcessor>();
                    movementProcessor.Initialize(uniqueID,this, m_playerMovement,(MovementModifier)modifier);
                    movementProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, movementProcessor);
                    break;
                case ModifierType.HEALTH:
                    var healthProcessor = this.gameObject.AddComponent<HealthModifierProcessor>();
                    healthProcessor.Initialize(uniqueID,this, m_playerHealth,(HealthModifier)modifier);
                    healthProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, healthProcessor);
                    break;
                case ModifierType.DAMAGE:
                    var damageProcessor = this.gameObject.AddComponent<DamageModifierProcessor>();
                    damageProcessor.Initialize(uniqueID,this, m_damageComputer,(DamageModifier)modifier);
                    damageProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, damageProcessor);
                    break;
                case ModifierType.TRIGGER_AFTER_GAME_EVENT:
                    var triggerProcessor = this.gameObject.AddComponent<TriggerAfterEventModifierProcessor>();
                    triggerProcessor.Initialize(uniqueID,this, (TriggerAfterEventModifier)modifier);
                    m_processorContainer.Add(uniqueID, triggerProcessor);
                    break;
                case ModifierType.ARMOR:
                    var armorProcessor = this.gameObject.AddComponent<ArmorModifierProcessor>();
                    armorProcessor.Initialize(uniqueID,this, (ArmorModifier)modifier,m_playerHealth);
                    armorProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, armorProcessor);
                    break;
                case ModifierType.COIN:
                    var coinProcessor = this.gameObject.AddComponent<CoinModifierProcessor>();
                    coinProcessor.Initialize(uniqueID,this, (CoinModifier)modifier);
                    coinProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, coinProcessor);
                    break;
            }
        }

        public void RemoveProcessor(ModifierProcessor modifierProcessor)
        {
            if (m_processorContainer.ContainsKey(modifierProcessor.Id))
            {
                m_processorContainer.Remove(modifierProcessor.Id);
                DestroyImmediate(modifierProcessor);
            }
        }

        public void UnregisterModifier(Modifier info)
        {
            var processor = m_processorContainer.FirstOrDefault(x=>(x.Value.Modifier == info)); 
            processor.Value.StopModifier();
        }
        
        private void OnReceiveGameEvent(GameEventType eventType)
        {
            foreach (var processor in m_processorContainer.Values)
            {
                if (processor.Modifier.ModifierType == ModifierType.TRIGGER_AFTER_GAME_EVENT)
                {
                    if (((TriggerAfterEventModifier)processor.Modifier).EventTypeToTrigger == eventType)
                    {
                        processor.StartModifier();
                        if (((TriggerAfterEventModifier)processor.Modifier).TriggerOnce)
                        {
                            RemoveProcessor(processor);
                        }
                    }
                }
            }
        }
    }
}

