using System;
using System.Collections.Generic;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "AttributeTierProgression", menuName = "SGGames/Progression/Attribute Tier Progression")]
    public class AttributeRarityProgressionData : ScriptableObject
    {
        [SerializeField] private AttributeRarityProgression[] m_progressions;

        public AttributeTier GetAttributeTierAtLevel(int level, float chance)
        {
            var progression = m_progressions[level];
            if (chance <= progression.TierChance[0])
            {
                return AttributeTier.Tier1;
            }
            if (chance <= progression.TierChance[1])
            {
                return AttributeTier.Tier2;
            }
            if (chance <= progression.TierChance[2])
            {
                return AttributeTier.Tier3;
            }
            return AttributeTier.Tier4;
        }

        public void SetData(List<AttributeRarityProgression> attributeTiers)
        {
            m_progressions = new AttributeRarityProgression[attributeTiers.Count];
            m_progressions = attributeTiers.ToArray();
        }
    }

    [Serializable]
    public struct AttributeRarityProgression
    {
        public int Level;
        public float[] TierChance;
    }
}

