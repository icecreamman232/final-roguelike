using SGGames.Scripts.Manager;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class CoinModifierProcessor : ModifierProcessor
    {
        public void Initialize(string id, ModifierHandler modifierHandler, CoinModifier modifier)
        {
            m_id = id;
            m_handler = modifierHandler;
            m_modifier = modifier;
        }

        public override void StartModifier()
        {
            base.StartModifier();
            switch (((CoinModifier)m_modifier).CoinModifierType)
            {
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ENEMY:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(((CoinModifier)m_modifier).ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_CHEST:
                    CurrencyManager.Instance.AddExtraCoinForChest(((CoinModifier)m_modifier).ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(((CoinModifier)m_modifier).ModifierValue);
                    CurrencyManager.Instance.AddExtraCoinForChest(((CoinModifier)m_modifier).ModifierValue);
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((CoinModifier)m_modifier).CoinModifierType} " +
                      $"- Value:{((CoinModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            base.StopModifier();
            switch (((CoinModifier)m_modifier).CoinModifierType)
            {
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ENEMY:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(-((CoinModifier)m_modifier).ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_CHEST:
                    CurrencyManager.Instance.AddExtraCoinForChest(-((CoinModifier)m_modifier).ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(-((CoinModifier)m_modifier).ModifierValue);
                    CurrencyManager.Instance.AddExtraCoinForChest(-((CoinModifier)m_modifier).ModifierValue);
                    break;
            }
            
            m_handler.RemoveProcessor(this);
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((CoinModifier)m_modifier).CoinModifierType} " +
                      $"- Value:{((CoinModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}

