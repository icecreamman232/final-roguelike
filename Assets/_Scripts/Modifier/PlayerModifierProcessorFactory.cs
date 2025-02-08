using System;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerModifierProcessorFactory
    {
        public static ModifierProcessor Create(string id, PlayerModifierHandler handler, Modifier modifier)
        {
            ModifierProcessor processor = null;
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    processor = handler.gameObject.AddComponent<PlayerMovementModifierProcessor>();
                    break;
                case ModifierType.HEALTH:
                    processor = handler.gameObject.AddComponent<PlayerHealthModifierProcessor>();
                    break;
                case ModifierType.DAMAGE:
                    processor = handler.gameObject.AddComponent<PlayerDamageModifierProcessor>();
                    break;
                case ModifierType.GAME_EVENT:
                    processor = handler.gameObject.AddComponent<PlayerGameEventModifierProcessor>();
                    break;
                case ModifierType.ARMOR:
                    processor = handler.gameObject.AddComponent<PlayerArmorModifierProcessor>();
                    break;
                case ModifierType.COIN:
                    processor = handler.gameObject.AddComponent<PlayerCoinModifierProcessor>();
                    break;
                case ModifierType.PLAYER_EVENT:
                    processor = handler.gameObject.AddComponent<PlayerEventModifierProcessor>();
                    break;
                case ModifierType.HEALING:
                    processor = handler.gameObject.AddComponent<PlayerHealingModifierProcessor>();
                    break;
                case ModifierType.ATTRIBUTE:
                    processor = handler.gameObject.AddComponent<PlayerAttributeModifierProcessor>();
                    break;
                case ModifierType.MANA:
                    processor = handler.gameObject.AddComponent<PlayerManaModifierProcessor>();
                    break;
                case ModifierType.CONVERT_MANA_TO_DAMAGE:
                    processor = handler.gameObject.AddComponent<PlayerConvertManaToDamageModifierProcessor>();
                    break;
                case ModifierType.HEALTH_CONDITION:
                    processor = handler.gameObject.AddComponent<PlayerHealthConditionModifierProcessor>();
                    break;
                case ModifierType.WEAPON_TYPE_BASED:
                    processor = handler.gameObject.AddComponent<PlayerWeaponTypeModifierProcessor>();
                    break;
                case ModifierType.ATTACK_TIME:
                    processor = handler.gameObject.AddComponent<PlayerAttackTimeModifierProcessor>();
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

