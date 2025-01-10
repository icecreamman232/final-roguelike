using System;
using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ModifierProcessorFactory
    {
        public static ModifierProcessor Create(string id, ModifierHandler handler, Modifier modifier)
        {
            ModifierProcessor processor = null;
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    processor = handler.gameObject.AddComponent<MovementModifierProcessor>();
                    break;
                case ModifierType.HEALTH:
                    processor = handler.gameObject.AddComponent<HealthModifierProcessor>();
                    break;
                case ModifierType.DAMAGE:
                    processor = handler.gameObject.AddComponent<DamageModifierProcessor>();
                    break;
                case ModifierType.GAME_EVENT:
                    processor = handler.gameObject.AddComponent<GameEventModifierProcessor>();
                    break;
                case ModifierType.ARMOR:
                    processor = handler.gameObject.AddComponent<ArmorModifierProcessor>();
                    break;
                case ModifierType.COIN:
                    processor = handler.gameObject.AddComponent<CoinModifierProcessor>();
                    break;
                case ModifierType.PLAYER_EVENT:
                    processor = handler.gameObject.AddComponent<PlayerEventModifierProcessor>();
                    break;
                case ModifierType.HEALING:
                    processor = handler.gameObject.AddComponent<HealingModifierProcessor>();
                    break;
                case ModifierType.ATTRIBUTE:
                    processor = handler.gameObject.AddComponent<AttributeModifierProcessor>();
                    break;
                case ModifierType.MANA:
                    processor = handler.gameObject.AddComponent<ManaModifierProcessor>();
                    break;
                case ModifierType.CONVERT_MANA_TO_DAMAGE:
                    processor = handler.gameObject.AddComponent<ConvertManaToDamageModifierProcessor>();
                    break;
                case ModifierType.HEALTH_CONDITION:
                    processor = handler.gameObject.AddComponent<HealthConditionModifierProcessor>();
                    break;
                case ModifierType.WEAPON_TYPE_BASED:
                    processor = handler.gameObject.AddComponent<WeaponTypeModifierProcessor>();
                    break;
                default:
                    throw new ArgumentException("Unknown modifier type");
            }
            processor.Initialize(id,handler,modifier);
            if (modifier.ModifierType != ModifierType.PLAYER_EVENT
                || modifier.ModifierType != ModifierType.GAME_EVENT)
            {
                if (modifier.InstantTrigger)
                {
                    processor.StartModifier();
                }
            }

            return processor;
        }
    }
}

