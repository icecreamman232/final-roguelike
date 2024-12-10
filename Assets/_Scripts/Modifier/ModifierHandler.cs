using System;
using System.Collections.Generic;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifier
{
    public class ModifierHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private List<MovementModifierProcessor> m_movementModifierProcessors;
        
        public void RegisterModifier(ModifierInfo info)
        {
            switch (info.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    var processor = this.gameObject.AddComponent<MovementModifierProcessor>();
                    processor.Initialize(this, m_playerMovement,(MovementModifierInfo)info);
                    processor.StartModifier();
                    m_movementModifierProcessors.Add(processor);
                    Debug.Log($"<color=orange>Registered Modifier: {info.ModifierType}</color>");
                    break;
                case ModifierType.HEALTH:
                    break;
                case ModifierType.DAMAGE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RemoveMovementModifierProcessor(MovementModifierProcessor processor)
        {
            if (!m_movementModifierProcessors.Contains(processor)) return;
            m_movementModifierProcessors.Remove(processor);
            DestroyImmediate(processor);
        }

        public void UnregisterModifier(ModifierInfo info)
        {
            
        }
    }
}

