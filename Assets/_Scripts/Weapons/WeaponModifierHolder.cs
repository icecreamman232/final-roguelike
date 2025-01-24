using SGGames.Scripts.Core;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.Weapons
{
    public class WeaponModifierHolder : MonoBehaviour
    {
        [SerializeField] private Modifier[] m_modifiers;

        public void ApplyModifiers()
        {
            var modifierHandler = ServiceLocator.GetService<PlayerModifierHandler>();
            foreach (var modifier in m_modifiers)
            {
                modifierHandler.RegisterModifier(modifier);
            }
        }

        public void RemoveModifiers()
        {
            var modifierHandler = ServiceLocator.GetService<PlayerModifierHandler>();
            foreach (var modifier in m_modifiers)
            {
                modifierHandler.UnregisterModifier(modifier);
            }
        }
    }
}
