using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Player;

namespace SGGames.Scripts.Modifiers
{
    public class WeaponTypeModifierProcessor : ModifierProcessor
    {
        private PlayerWeaponHandler m_playerWeaponHandler;
        public override void Initialize(string id, ModifierHandler modifierHandler, Modifier modifier)
        {
            base.Initialize(id, modifierHandler, modifier);
            m_playerWeaponHandler = ServiceLocator.GetService<PlayerWeaponHandler>();
            m_playerWeaponHandler.OnEquipWeapon += OnEquipWeapon;
        }

        private void OnEquipWeapon(WeaponData newWeaponData)
        {
            TriggerModifier(newWeaponData.WeaponCategory);
        }

        public override void StartModifier()
        {
            base.StartModifier();
            TriggerModifier(m_playerWeaponHandler.CurrentWeaponData.WeaponCategory);
        }

        public override void StopModifier()
        {
            m_playerWeaponHandler.OnEquipWeapon -= OnEquipWeapon;
            RemoveModifier();
            base.StopModifier();
        }

        private void TriggerModifier(WeaponCategory category)
        {
            var weaponTypeModifier = m_modifier as WeaponTypeConditionModifier;
            if (category == WeaponCategory.Melee)
            {
                m_handler.RegisterModifier(weaponTypeModifier.ModifierForMeleeWeapon);
            }
            else
            {
                m_handler.RegisterModifier(weaponTypeModifier.ModifierForRangeWeapon);
            }
        }

        private void RemoveModifier()
        {
            var weaponTypeModifier = m_modifier as WeaponTypeConditionModifier;
            m_handler.UnregisterModifier(weaponTypeModifier.ModifierForMeleeWeapon);
            m_handler.UnregisterModifier(weaponTypeModifier.ModifierForRangeWeapon);
        }
    }
}

