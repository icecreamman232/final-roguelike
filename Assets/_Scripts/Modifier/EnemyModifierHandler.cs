using System;
using System.Collections.Generic;
using System.Linq;
using SGGames.Scripts.Common;
using SGGames.Scripts.Enemies;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class EnemyModifierHandler : MonoBehaviour
    {
        private Dictionary<string, EnemyModifierProcessor> m_processorContainer;

        private EnemyMovement m_movement;
        
        public EnemyMovement EnemyMovement => m_movement;
        
        private void Start()
        {
            m_processorContainer = new Dictionary<string, EnemyModifierProcessor>();
            m_movement = GetComponentInParent<EnemyMovement>();
        }

        public void RegisterModifier(Modifier modifier)
        {
            if (IsExist(modifier)) return;
            
            var uniqueID = RandomController.GetUniqueID();
            var processor = EnemyModifierProcessorFactory.Create(uniqueID,this, modifier);
            if (processor == null) return;
            m_processorContainer.Add(uniqueID,processor);
            
        }
        
        public void UnregisterModifier(Modifier modifier)
        {
            var processor = m_processorContainer.FirstOrDefault(x=>(x.Value.Modifier == modifier));
            if (processor.Key == null) return;
            processor.Value.StopModifier();
        }
        
        public void StartProcessor(Modifier modifier)
        {
            var processor = m_processorContainer.FirstOrDefault(x=>(x.Value.Modifier == modifier));
            if (processor.Key == null) return;
            processor.Value.StartModifier();
        }
        
        public void RemoveProcessor(EnemyModifierProcessor modifierProcessor)
        {
            if (m_processorContainer.ContainsKey(modifierProcessor.Id))
            {
                m_processorContainer.Remove(modifierProcessor.Id);
                DestroyImmediate(modifierProcessor);
            }
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
    }
}


