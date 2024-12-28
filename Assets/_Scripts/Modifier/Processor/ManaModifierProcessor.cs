using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ManaModifierProcessor : ModifierProcessor
    {
        public override void StartModifier()
        {
            base.StartModifier();
            var manaModifier = ((ManaModifier)m_modifier);
            switch (manaModifier.ManaModifierType)
            {
                case ManaModifierType.Modify_Max_Mana:
                    m_handler.PlayerMana.AddMaxMana(manaModifier.ModifierValue);
                    break;
                case ManaModifierType.Modify_Current_Mana:
                    m_handler.PlayerMana.AddCurrentMana(manaModifier.ModifierValue);
                    break;
                case ManaModifierType.Modify_Max_Mana_For_Duration:
                    m_handler.PlayerMana.AddMaxMana(manaModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case ManaModifierType.Modify_Current_Mana_For_Duration:
                    m_handler.PlayerMana.AddCurrentMana(manaModifier.ModifierValue);
                    m_isProcessing = true;
                    break;
            }
        }

        public override void StopModifier()
        {
            var manaModifier = ((ManaModifier)m_modifier);

            switch (manaModifier.ManaModifierType)
            {
                case ManaModifierType.Modify_Max_Mana:
                    m_handler.PlayerMana.AddMaxMana(-manaModifier.ModifierValue);
                    break;
                case ManaModifierType.Modify_Current_Mana:
                    m_handler.PlayerMana.AddCurrentMana(-manaModifier.ModifierValue);
                    break;
                case ManaModifierType.Modify_Max_Mana_For_Duration:
                    m_handler.PlayerMana.AddMaxMana(-manaModifier.ModifierValue);
                    break;
                case ManaModifierType.Modify_Current_Mana_For_Duration:
                    m_handler.PlayerMana.AddCurrentMana(-manaModifier.ModifierValue);
                    break;
            }
            base.StopModifier();
        }
        
        protected override void Update()
        {
            if (!m_isProcessing) return;
            
            m_timer += Time.deltaTime;
            if (m_timer >= m_modifier.Duration)
            {
                m_timer = 0;
                StopModifier();
            }
        }
    }
}
