using System;

namespace SGGames.Scripts.Modifiers
{
    public class EnemyModifierProcessorFactory
    {
        public static EnemyModifierProcessor Create(string id, EnemyModifierHandler handler, Modifier modifier)
        {
            EnemyModifierProcessor processor = null;
            switch (modifier.ModifierType)
            {
                case ModifierType.MOVEMENT:
                    processor = handler.gameObject.AddComponent<EnemyMovementModifierProcessor>();
                    break;
                case ModifierType.ARMOR:
                    break;
                default:
                    throw new ArgumentException("Unknown modifier type");
            }

            if (processor != null)
            {
                processor.Initialize(id,handler,modifier);
                if (modifier.InstantTrigger)
                {
                    processor.StartModifier();
                }
            }
            return processor;
        }
    }
}

