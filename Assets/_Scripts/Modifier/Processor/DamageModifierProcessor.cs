using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class DamageModifierProcessor : ModifierProcessor
    {
       [SerializeField] private PlayerDamageComputer m_damageComputer;

       public void Initialize(string id, ModifierHandler handler, PlayerDamageComputer damageComputer, DamageModifier modifier)
       {
           m_id = id;
           m_handler = handler;
           m_damageComputer = damageComputer;
           m_modifier = modifier;
       }
       
       public override void StartModifier()
        {
            base.StartModifier();
            switch (((DamageModifier)m_modifier).DamageModifierType)
            {
                case DamageModifierType.ReduceDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(-((DamageModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case DamageModifierType.IncreaseDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(((DamageModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case DamageModifierType.MultiplyDamage_ForDuration:
                    m_damageComputer.UpdateMultiplyDamage(((DamageModifier)m_modifier).ModifierValue);
                    m_isProcessing = true;
                    break;
                case DamageModifierType.IncreaseDamage:
                    m_damageComputer.UpdateAdditionDamage(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.ReduceDamage:
                    m_damageComputer.UpdateAdditionDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.MultiplyDamage:
                    m_damageComputer.UpdateMultiplyDamage(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.IncreaseCriticalChance:
                    m_damageComputer.AddCriticalChance(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.IncreaseCriticalDamage:
                    m_damageComputer.AddCriticalDamage(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.ReduceCriticalChance:
                    m_damageComputer.AddCriticalChance(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.ReduceCriticalDamage:
                    m_damageComputer.AddCriticalDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((DamageModifier)m_modifier).DamageModifierType} " +
                      $"- Value:{((DamageModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((DamageModifier)m_modifier).DamageModifierType} " +
                      $"- Value:{((DamageModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            base.StopModifier();
            switch (((DamageModifier)m_modifier).DamageModifierType)
            {
                case DamageModifierType.ReduceDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.IncreaseDamage_ForDuration:
                    m_damageComputer.UpdateAdditionDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.MultiplyDamage_ForDuration:
                    m_damageComputer.UpdateMultiplyDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.ReduceDamage:
                    m_damageComputer.UpdateAdditionDamage(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.IncreaseDamage:
                    m_damageComputer.UpdateAdditionDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.MultiplyDamage:
                    m_damageComputer.UpdateMultiplyDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.IncreaseCriticalChance:
                    m_damageComputer.AddCriticalChance(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.IncreaseCriticalDamage:
                    m_damageComputer.AddCriticalDamage(-((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.ReduceCriticalChance:
                    m_damageComputer.AddCriticalChance(((DamageModifier)m_modifier).ModifierValue);
                    break;
                case DamageModifierType.ReduceCriticalDamage:
                    m_damageComputer.AddCriticalDamage(((DamageModifier)m_modifier).ModifierValue);
                    break;
                
            }

            m_isProcessing = false;
            m_handler.RemoveProcessor(this);
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

