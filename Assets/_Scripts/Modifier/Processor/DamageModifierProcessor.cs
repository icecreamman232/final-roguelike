using SGGames.Scripts.Damages;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class DamageModifierProcessor : ModifierProcessor
    {
       [SerializeField] private PlayerDamageComputer m_damageComputer;
        [SerializeField] private int m_damageInfluencerID;
       
       
       public override void Initialize(string id, ModifierHandler handler, Modifier modifier)
       {
           base.Initialize(id, handler, modifier);
           m_damageComputer = handler.PlayerDamageComputer;
       }
       
       public override void StartModifier()
        {
            base.StartModifier();
            var damageModifier = ((DamageModifier)m_modifier);
            
            switch (damageModifier.DamageModifierType)
            {
                case DamageModifierType.ReduceDamage_ForDuration:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                            DamageInfluencerType.ADD_DAMAGE, 
                            damageModifier.ChanceToCause,
                            -damageModifier.ModifierValue));
                    m_isProcessing = true;
                    break;
                case DamageModifierType.IncreaseDamage_ForDuration:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.ADD_DAMAGE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    m_isProcessing = true;
                    break;
                case DamageModifierType.MultiplyDamage_ForDuration:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.MULTIPLY_DAMAGE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    m_isProcessing = true;
                    break;
                case DamageModifierType.IncreaseDamage:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.ADD_DAMAGE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    break;
                case DamageModifierType.ReduceDamage:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.ADD_DAMAGE, 
                        damageModifier.ChanceToCause,
                        -damageModifier.ModifierValue));
                    break;
                case DamageModifierType.MultiplyDamage:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.MULTIPLY_DAMAGE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    break;
                case DamageModifierType.IncreaseCriticalChance:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.CRITICAL_CHANCE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    break;
                case DamageModifierType.IncreaseCriticalDamage:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.CRITICAL_DAMAGE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    break;
                case DamageModifierType.ReduceCriticalChance:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.CRITICAL_CHANCE, 
                        damageModifier.ChanceToCause,
                        -damageModifier.ModifierValue));
                    break;
                case DamageModifierType.ReduceCriticalDamage:
                    m_damageInfluencerID = m_damageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                        DamageInfluencerType.CRITICAL_DAMAGE, 
                        damageModifier.ChanceToCause,
                        damageModifier.ModifierValue));
                    break;
            }
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{damageModifier.DamageModifierType} " +
                      $"- Value:{damageModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    
        public override void StopModifier()
        {
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Type:{((DamageModifier)m_modifier).DamageModifierType} " +
                      $"- Value:{((DamageModifier)m_modifier).ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
            
            base.StopModifier();
            
            m_damageComputer.RemoveDamageInfluencer(m_damageInfluencerID);

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

