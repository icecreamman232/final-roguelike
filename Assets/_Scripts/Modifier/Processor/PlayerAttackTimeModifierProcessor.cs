
using SGGames.Scripts.Core;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Modifiers
{
    public class PlayerAttackTimeModifierProcessor : ModifierProcessor
    {
        private PlayerWeaponHandler m_playerWeaponHandler;
        
        public override void Initialize(string id, PlayerModifierHandler modifierHandler, Modifier modifier)
        {
            m_playerWeaponHandler = ServiceLocator.GetService<PlayerWeaponHandler>();
            base.Initialize(id, modifierHandler, modifier);
        }

        public override void StartModifier()
        {
            base.StartModifier();
            var atkTimeModifier = ((AttackTimeModifier)m_modifier); 
            var currentAtkTime = m_playerWeaponHandler.CurrentWeapon.CurrentDelayBetweenShots;
            currentAtkTime += atkTimeModifier.ModifierValue;
            m_playerWeaponHandler.ApplyAttackSpeedOnCurrentWeapon(currentAtkTime);
            
            Debug.Log($"<color=green>Start Modifier Category:{m_modifier.ModifierType} " +
                      $"- Value:{atkTimeModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }

        public override void StopModifier()
        {
            var atkTimeModifier = ((AttackTimeModifier)m_modifier); 
            var currentAtkTime = m_playerWeaponHandler.CurrentWeapon.CurrentDelayBetweenShots;
            currentAtkTime += -atkTimeModifier.ModifierValue;
            m_playerWeaponHandler.ApplyAttackSpeedOnCurrentWeapon(currentAtkTime);
            base.StopModifier();
            
            Debug.Log($"<color=green>Stop Modifier Category:{m_modifier.ModifierType} " +
                      $"- Value:{atkTimeModifier.ModifierValue}" +
                      $"- Duration:{m_modifier.Duration}</color> ");
        }
    }
}

