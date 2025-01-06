using SGGames.Scripts.Events;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class MoneyTalkAbility : Ability, ISelectableAbility
    {
        [SerializeField] private DamageModifier m_damageModifier;
        [SerializeField] private float m_coinToDamageConvertRate;
        [SerializeField] private float m_currentBonusDamage;
        [SerializeField] private IntEvent m_updateCoinCounterEvent;

        private ModifierHandler m_modifierHandler;

        public void Initialize(ModifierHandler modifierHandler)
        {
            m_modifierHandler = modifierHandler;
            OnUpdateCoin(CurrencyManager.Instance.CurrentCoin);
        }
        
        protected override void Start()
        {
            base.Start();
            m_damageModifier.ModifierValue = 0;
            m_updateCoinCounterEvent.AddListener(OnUpdateCoin);
        }

        private void OnDestroy()
        {
            m_damageModifier.ModifierValue = 0;
            m_updateCoinCounterEvent.RemoveListener(OnUpdateCoin);
        }

        private void OnUpdateCoin(int totalCoins)
        {
            m_abilityState = AbilityState.TRIGGERING;
            m_currentBonusDamage = Mathf.Round(totalCoins * m_coinToDamageConvertRate);
            m_damageModifier.ModifierValue = m_currentBonusDamage;
            m_modifierHandler.UnregisterModifier(m_damageModifier);
            m_modifierHandler.RegisterModifier(m_damageModifier);
        }
    }
}

