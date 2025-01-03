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
        [Header("Events")] 
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private PlayerEvent m_playerEvent;
        [Header("Processors")]
        private Dictionary<string,ModifierProcessor> m_processorContainer;

        private PlayerAttributeController m_playerAttributeController;
        private PlayerMovement m_playerMovement;
        private PlayerHealth m_playerHealth;
        private PlayerMana m_playerMana;
        private PlayerDamageComputer m_damageComputer;
        
        public PlayerAttributeController PlayerAttributeController => m_playerAttributeController;
        public PlayerMovement PlayerMovement => m_playerMovement;
        public PlayerHealth PlayerHealth => m_playerHealth;
        public PlayerMana PlayerMana => m_playerMana;
        public PlayerDamageComputer PlayerDamageComputer => m_damageComputer;
        
        private void Start()
        {
            m_playerAttributeController = GetComponentInParent<PlayerAttributeController>();
            m_playerHealth = GetComponentInParent<PlayerHealth>();
            m_playerMana = GetComponentInParent<PlayerMana>();
            m_playerMovement = GetComponentInParent<PlayerMovement>();
            m_damageComputer = GetComponentInParent<PlayerDamageComputer>();
            
            
            m_gameEvent.AddListener(OnReceiveGameEvent);
            m_playerEvent.AddListener(OnReceivePlayerEvent);
            m_processorContainer = new Dictionary<string, ModifierProcessor>();
        }

        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnReceiveGameEvent);
            m_playerEvent.RemoveListener(OnReceivePlayerEvent);
        }

        public void RegisterModifier(Modifier modifier)
        {
            var uniqueID = RandomController.GetUniqueID();
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    var movementProcessor = this.gameObject.AddComponent<MovementModifierProcessor>();
                    movementProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        movementProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, movementProcessor);
                    break;
                case ModifierType.HEALTH:
                    var healthProcessor = this.gameObject.AddComponent<HealthModifierProcessor>();
                    healthProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        healthProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, healthProcessor);
                    break;
                case ModifierType.DAMAGE:
                    var damageProcessor = this.gameObject.AddComponent<DamageModifierProcessor>();
                    damageProcessor.Initialize(uniqueID, this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        damageProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, damageProcessor);
                    break;
                case ModifierType.GAME_EVENT:
                    var gameEventProcessor = this.gameObject.AddComponent<GameEventModifierProcessor>();
                    gameEventProcessor.Initialize(uniqueID,this, modifier);
                    m_processorContainer.Add(uniqueID, gameEventProcessor);
                    break;
                case ModifierType.PLAYER_EVENT:
                    var playerEventProcessor = this.gameObject.AddComponent<PlayerEventModifierProcessor>();
                    playerEventProcessor.Initialize(uniqueID,this, modifier);
                    m_processorContainer.Add(uniqueID, playerEventProcessor);
                    break;
                case ModifierType.ARMOR:
                    var armorProcessor = this.gameObject.AddComponent<ArmorModifierProcessor>();
                    armorProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        armorProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, armorProcessor);
                    break;
                case ModifierType.COIN:
                    var coinProcessor = this.gameObject.AddComponent<CoinModifierProcessor>();
                    coinProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        coinProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, coinProcessor);
                    break;
                case ModifierType.HEALING:
                    var healingProcessor = this.gameObject.AddComponent<HealingModifierProcessor>();
                    healingProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        healingProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, healingProcessor);
                    break;
                case ModifierType.ATTRIBUTE:
                    var attributeProcessor = this.gameObject.AddComponent<AttributeModifierProcessor>();
                    attributeProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        attributeProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, attributeProcessor);
                    break;
                case ModifierType.MANA: 
                    var manaProcessor = this.gameObject.AddComponent<ManaModifierProcessor>();
                    manaProcessor.Initialize(uniqueID,this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        manaProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, manaProcessor);
                    break;
                case ModifierType.CONVERT_MANA_TO_DAMAGE:
                    var manaToDamageProcessor = this.gameObject.AddComponent<ConvertManaToDamageModifierProcessor>();
                    manaToDamageProcessor.Initialize(uniqueID, this, modifier);
                    if (modifier.InstantTrigger)
                    {
                        manaToDamageProcessor.StartModifier();
                    }
                    m_processorContainer.Add(uniqueID, manaToDamageProcessor);
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

        public void UnregisterModifier(Modifier modifier)
        {
            var processor = m_processorContainer.FirstOrDefault(x=>(x.Value.Modifier == modifier)); 
            processor.Value.StopModifier();
        }

        public void StartProcessor(Modifier modifier)
        {
            var processor = m_processorContainer.FirstOrDefault(x=>(x.Value.Modifier == modifier));
            processor.Value.StartModifier();
        }
        
        private void OnReceiveGameEvent(GameEventType eventType)
        {
            foreach (var processor in m_processorContainer.Values)
            {
                if (processor.Modifier.ModifierType == ModifierType.GAME_EVENT)
                {
                    if (((GameEventModifier)processor.Modifier).EventTypeToTrigger == eventType)
                    {
                        processor.StartModifier();
                        if (((GameEventModifier)processor.Modifier).TriggerOnce)
                        {
                            RemoveProcessor(processor);
                        }
                    }
                }
            }
        }
        
        private void OnReceivePlayerEvent(PlayerEventType eventType)
        {
            foreach (var processor in m_processorContainer.Values)
            {
                if (processor.Modifier.ModifierType == ModifierType.PLAYER_EVENT)
                {
                    if (((PlayerEventModifier)processor.Modifier).EventTypeToTrigger == eventType)
                    {
                        processor.StartModifier();
                        if (((PlayerEventModifier)processor.Modifier).TriggerOnce)
                        {
                            RemoveProcessor(processor);
                        }
                    }
                }
            }
        }
    }
}

