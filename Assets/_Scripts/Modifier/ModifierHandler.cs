using System;
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
            var processor = ModifierProcessorFactory.Create(uniqueID,this, modifier);
            m_processorContainer.Add(uniqueID,processor);
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

        public bool IsExist(Modifier modifier)
        {
            foreach (var dictValue in m_processorContainer)
            {
                if (dictValue.Value.Modifier == modifier)
                {
                    return true;
                }
            }
            return false;
        }
        

        /// <summary>
        /// Find a modifier by specific query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Modifier QueryModifier(Func<Modifier,bool> query)
        {
            foreach (var dictValue in m_processorContainer)
            {
                if (query(dictValue.Value.Modifier))
                {
                    return dictValue.Value.Modifier;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a processor by modifier the handler process
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ModifierProcessor QueryProcessor(Func<Modifier, bool> query)
        {
            foreach (var dictValue in m_processorContainer)
            {
                if (query(dictValue.Value.Modifier))
                {
                    return dictValue.Value;
                }
            }

            return null;
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

