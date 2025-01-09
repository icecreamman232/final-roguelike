using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class HealthPotionPicker : Pickable
    {
        [SerializeField] private HealthPotionType m_potionType;
        [SerializeField] private float m_healingAmount;
        
        
        protected override void Picked(Transform playerTransform)
        {
            var playerHealth = ServiceLocator.GetService<PlayerHealth>();
            playerHealth.HealingFlatAmount(m_healingAmount);
            base.Picked(playerTransform);
        }
    }
}

