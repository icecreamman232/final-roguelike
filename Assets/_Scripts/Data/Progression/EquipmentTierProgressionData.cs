using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "Equipment Tier Progression Data", menuName = "SGGames/Progression/Equipment Tier Progression", order = 1)]
    public class EquipmentTierProgressionData : ScriptableObject
    {
        [SerializeField] private EquipmentTierProgression[] m_EquipmentTierProgressions;

        public Rarity GetEquipmentRarity(int areaIndex)
        {
            foreach (var progression in m_EquipmentTierProgressions)
            {
                if (progression.AreaIndex == areaIndex)
                {
                    var chance = Random.Range(0, 101);
                    return GetEquipment(chance, progression);
                }
            }

            return Rarity.Common;
        }

        private Rarity GetEquipment(float chance, EquipmentTierProgression progression)
        {
            if (chance <= progression.TierChance[0])
            {
                return Rarity.Common;
            }
            if (chance <= progression.TierChance[0]  + progression.TierChance[1])
            {
                return Rarity.Uncommon;
            }
            if (chance <= progression.TierChance[0]  + progression.TierChance[1] + progression.TierChance[2])
            {
                return Rarity.Rare;
            }
            if (chance <= progression.TierChance[0]  + progression.TierChance[1] + progression.TierChance[2] + progression.TierChance[3])
            {
                return Rarity.Epic;
            }
            return Rarity.Legendary;
        }
        
        public void SetData(List<EquipmentTierProgression> equipmentRarities)
        {
            m_EquipmentTierProgressions = new EquipmentTierProgression[equipmentRarities.Count];
            m_EquipmentTierProgressions = equipmentRarities.ToArray();
        }
    }

    [Serializable]
    public struct EquipmentTierProgression
    {
        public int AreaIndex;
        public float[] TierChance; 
    }
}
