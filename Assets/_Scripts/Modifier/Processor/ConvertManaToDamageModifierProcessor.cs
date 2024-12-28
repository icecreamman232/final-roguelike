
using SGGames.Scripts.Damages;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class ConvertManaToDamageModifierProcessor : ModifierProcessor
    {
        [SerializeField] private int m_damageInfluencerID;
        
        public override void StartModifier()
        {
            base.StartModifier();
            
            m_handler.PlayerMana.OnChangeMaxMana += OnChangeMaxMana;
                
            var damageToAdd = Mathf.RoundToInt(m_handler.PlayerMana.CurrentMana * ((ConvertManaToDamageModifier)m_modifier).ManaToDamageRate);
                
            m_damageInfluencerID = m_handler.PlayerDamageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                DamageInfluencerType.ADD_DAMAGE, 
                100, damageToAdd));
                
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      "- Type:Convert Mana To Damage" +
                      $"- Value:{damageToAdd}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            m_handler.PlayerMana.OnChangeMaxMana -= OnChangeMaxMana;
            m_handler.PlayerDamageComputer.RemoveDamageInfluencer(m_damageInfluencerID);
            
            base.StopModifier();
            
            Debug.Log($"<color=red>Stop Modifier Category:{m_modifier.ModifierType} " +
                      "- Type:Convert Mana To Damage" +
                      $"- Value:" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        private void OnChangeMaxMana(float maxMana)
        {
            m_handler.PlayerDamageComputer.RemoveDamageInfluencer(m_damageInfluencerID);
            
            var damageToAdd = Mathf.RoundToInt(m_handler.PlayerMana.CurrentMana * ((ConvertManaToDamageModifier)m_modifier).ManaToDamageRate);
                
            m_damageInfluencerID = m_handler.PlayerDamageComputer.AddNewDamageInfluencer(new DamageInfluencer(
                DamageInfluencerType.ADD_DAMAGE, 
                100, damageToAdd));
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      "- Type:Convert Mana To Damage" +
                      $"- Value:{damageToAdd}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}


