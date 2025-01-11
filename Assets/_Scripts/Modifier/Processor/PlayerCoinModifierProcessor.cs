using SGGames.Scripts.Manager;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerCoinModifierProcessor : ModifierProcessor
    {
        public override void StartModifier()
        {
            var coinModifier = ((CoinModifier)m_modifier);
            
            base.StartModifier();
            switch (coinModifier.CoinModifierType)
            {
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ENEMY:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(coinModifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_CHEST:
                    CurrencyManager.Instance.AddExtraCoinForChest(coinModifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(coinModifier.ModifierValue);
                    CurrencyManager.Instance.AddExtraCoinForChest(coinModifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_COIN_FOR_ENEMY_MULTIPLIER:
                    CurrencyManager.Instance.AddExtraCoinEnemyMultiplier(coinModifier.ModifierValue/100f);
                    break;
                case CoinModifierType.ADD_COIN_FOR_CHEST_MULTIPLIER:
                    CurrencyManager.Instance.AddExtraCoinChestMultiplier(coinModifier.ModifierValue/100f);
                    break;
                case CoinModifierType.ADD_COIN_MULTIPLIER_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinEnemyMultiplier(coinModifier.ModifierValue/100f);
                    CurrencyManager.Instance.AddExtraCoinChestMultiplier(coinModifier.ModifierValue/100f);
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{coinModifier.CoinModifierType} " +
                      $"- Value:{coinModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            var coinModifier = ((CoinModifier)m_modifier);
            switch (coinModifier.CoinModifierType)
            {
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ENEMY:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(-coinModifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_CHEST:
                    CurrencyManager.Instance.AddExtraCoinForChest(-coinModifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(-coinModifier.ModifierValue);
                    CurrencyManager.Instance.AddExtraCoinForChest(-coinModifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_COIN_FOR_ENEMY_MULTIPLIER:
                    CurrencyManager.Instance.AddExtraCoinEnemyMultiplier(-coinModifier.ModifierValue/100f);
                    break;
                case CoinModifierType.ADD_COIN_FOR_CHEST_MULTIPLIER:
                    CurrencyManager.Instance.AddExtraCoinChestMultiplier(-coinModifier.ModifierValue/100f);
                    break;
                case CoinModifierType.ADD_COIN_MULTIPLIER_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinEnemyMultiplier(-coinModifier.ModifierValue/100f);
                    CurrencyManager.Instance.AddExtraCoinChestMultiplier(-coinModifier.ModifierValue/100f);
                    break;
            }
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{coinModifier.CoinModifierType} " +
                      $"- Value:{coinModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            base.StopModifier();
        }
    }
}

