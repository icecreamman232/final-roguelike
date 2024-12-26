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

        public PlayerMovement PlayerMovement => m_playerMovement;
        public PlayerHealth PlayerHealth => m_playerHealth;
        public PlayerDamageComputer PlayerDamageComputer => m_damageComputer;
        
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
                    movementProcessor.Initialize(uniqueID,this, modifier);
                    movementProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, movementProcessor);
                    break;
                case ModifierType.HEALTH:
                    var healthProcessor = this.gameObject.AddComponent<HealthModifierProcessor>();
                    healthProcessor.Initialize(uniqueID,this, modifier);
                    healthProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, healthProcessor);
                    break;
                case ModifierType.DAMAGE:
                    var damageProcessor = this.gameObject.AddComponent<DamageModifierProcessor>();
                    damageProcessor.Initialize(uniqueID,this, modifier);
                    damageProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, damageProcessor);
                    break;
                case ModifierType.TRIGGER_AFTER_GAME_EVENT:
                    var triggerProcessor = this.gameObject.AddComponent<TriggerAfterEventModifierProcessor>();
                    triggerProcessor.Initialize(uniqueID,this, modifier);
                    m_processorContainer.Add(uniqueID, triggerProcessor);
                    break;
                case ModifierType.ARMOR:
                    var armorProcessor = this.gameObject.AddComponent<ArmorModifierProcessor>();
                    armorProcessor.Initialize(uniqueID,this, modifier);
                    armorProcessor.StartModifier();
                    m_processorContainer.Add(uniqueID, armorProcessor);
                    break;
                case ModifierType.COIN:
                    var coinProcessor = this.gameObject.AddComponent<CoinModifierProcessor>();
                    coinProcessor.Initialize(uniqueID,this, modifier);
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
                    if (((TriggerAfterGameEventModifier)processor.Modifier).EventTypeToTrigger == eventType)
                    {
                        processor.StartModifier();
                        if (((TriggerAfterGameEventModifier)processor.Modifier).TriggerOnce)
                        {
                            RemoveProcessor(processor);
                        }
                    }
                }
            }
        }
    }
}

