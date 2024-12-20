using System;
using SGGames.Scripts.Manager;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class CoinModifierProcessor : ModifierProcessor
    {
        [SerializeField] private CoinModifier m_modifier;
        private ModifierHandler m_handler;
        
        public CoinModifier Modifier => m_modifier;

        public void Initialize(ModifierHandler modifierHandler, CoinModifier modifier)
        {
            m_handler = modifierHandler;
            m_modifier = modifier;
        }

        public override void StartModifier()
        {
            base.StartModifier();
            switch (m_modifier.CoinModifierType)
            {
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ENEMY:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(m_modifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_CHEST:
                    CurrencyManager.Instance.AddExtraCoinForChest(m_modifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(m_modifier.ModifierValue);
                    CurrencyManager.Instance.AddExtraCoinForChest(m_modifier.ModifierValue);
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.CoinModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            base.StopModifier();
            switch (m_modifier.CoinModifierType)
            {
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ENEMY:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(-m_modifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_CHEST:
                    CurrencyManager.Instance.AddExtraCoinForChest(-m_modifier.ModifierValue);
                    break;
                case CoinModifierType.ADD_EXTRA_COIN_FOR_ALL:
                    CurrencyManager.Instance.AddExtraCoinForEnemy(-m_modifier.ModifierValue);
                    CurrencyManager.Instance.AddExtraCoinForChest(-m_modifier.ModifierValue);
                    break;
            }
            
            m_handler.RemoveCoinModifierProcessor(this);
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.CoinModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}

