using System;
using System.Collections.Generic;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "AttributeTierProgression", menuName = "SGGames/Progression/Attribute Tier Progression", order = 0)]
    public class AttributeTierProgressionData : ScriptableObject
    {
        [SerializeField] private AttributeTierProgression[] m_progressions;

        public AttributeTier GetAttributeTierAtLevel(int level, float chance)
        {
            var progression = m_progressions[level-1];
            if (chance <= progression.TierChance[0])
            {
                return AttributeTier.Tier1;
            }
            if (chance <= progression.TierChance[0]  + progression.TierChance[1])
            {
                return AttributeTier.Tier2;
            }
            if (chance <= progression.TierChance[0]  + progression.TierChance[1] + progression.TierChance[2])
            {
                return AttributeTier.Tier3;
            }
            return AttributeTier.Tier4;
        }

        public void SetData(List<AttributeTierProgression> attributeTiers)
        {
            m_progressions = new AttributeTierProgression[attributeTiers.Count];
            m_progressions = attributeTiers.ToArray();
        }
    }

    [Serializable]
    public struct AttributeTierProgression
    {
        public int Level;
        public float[] TierChance;
    }
}

