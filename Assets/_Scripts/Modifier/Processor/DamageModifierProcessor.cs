using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class DamageModifierProcessor : ModifierProcessor
    {
       [SerializeField] private DamageModifier m_modifier;
       [SerializeField] private PlayerDamageComputer m_damageComputer;

       private float m_timer;
       private ModifierHandler m_handler;
       
       public void Initialize(ModifierHandler handler, PlayerDamageComputer damageComputer, DamageModifier modifier)
       {
           m_handler = handler;
           m_damageComputer = damageComputer;
           m_modifier = modifier;
       }
       
       public override void StartModifier()
        {
            base.StartModifier();
            switch (m_modifier.DamageModifierType)
            {
                case DamageModifierType.ReduceDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(-m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case DamageModifierType.IncreaseDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case DamageModifierType.MultiplyDamage_ForDuration:
                    m_damageComputer.UpdateMultiplyDamage(m_modifier.ModifierValue);
                    m_isProcessing = true;
                    break;
                case DamageModifierType.IncreaseDamage:
                    m_damageComputer.UpdateAdditionDamage(m_modifier.ModifierValue);
                    break;
                case DamageModifierType.ReduceDamage:
                    m_damageComputer.UpdateAdditionDamage(-m_modifier.ModifierValue);
                    break;
                case DamageModifierType.MultiplyDamage:
                    m_damageComputer.UpdateMultiplyDamage(m_modifier.ModifierValue);
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.DamageModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{m_modifier.DamageModifierType} " +
                      $"- Value:{m_modifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            base.StopModifier();
            switch (m_modifier.DamageModifierType)
            {
                case DamageModifierType.ReduceDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(m_modifier.ModifierValue);
                    break;
                case DamageModifierType.IncreaseDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(-m_modifier.ModifierValue);
                    break;
                case DamageModifierType.MultiplyDamage_ForDuration:
                    m_damageComputer.UpdateMultiplyDamage(-m_modifier.ModifierValue);
                    break;
                case DamageModifierType.ReduceDamage:
                    m_damageComputer.UpdateAdditionDamage(m_modifier.ModifierValue);
                    break;
                case DamageModifierType.IncreaseDamage:
                    m_damageComputer.UpdateAdditionDamage(-m_modifier.ModifierValue);
                    break;
                case DamageModifierType.MultiplyDamage:
                    m_damageComputer.UpdateMultiplyDamage(-m_modifier.ModifierValue);
                    break;
                
            }

            m_isProcessing = false;
            m_handler.RemoveDamageModifierProcessor(this);
        }

        protected override void Update()
        {
            if (!m_isProcessing) return;
            
            m_timer += Time.deltaTime;
            if (m_timer >= m_modifier.Duration)
            {
                StopModifier();
            }
        }
    } 
}

