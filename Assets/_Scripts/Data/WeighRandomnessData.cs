using System;
using SGGames.Scripts.Attribute;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [Serializable]
    public class WeightData
    {
        /// <summary>
        /// Use for display purpose only
        /// </summary>
        [SerializeField] private string m_eventName;
        public float Weight;
        [SerializeField] [ReadOnly] private float m_lowerChance;
        [SerializeField] [ReadOnly] private float m_upperChance;

        public void SetUpperChance(float upperChance)
        {
            m_upperChance = upperChance;
        }

        public void SetLowerChance(float lowerChance)
        {
            m_lowerChance = lowerChance;
        }

        public bool CanTrigger(float chance)
        {
            return chance >= m_lowerChance && chance <= m_upperChance;
        }
    }
    
    [CreateAssetMenu(fileName = "WeighRandomnessData", menuName = "SGGames/Data/Random Data")]
    public class WeighRandomnessData : ScriptableObject
    {
        public WeightData[] Weights;
        
        public int GetTriggerEventIndex(float chance)
        {
            chance /= 100f; //Convert from percent
            for (int i = 0; i < Weights.Length; i++)
            {
                if (Weights[i].CanTrigger(chance))
                {
                    return i;
                }
            }

            return -1;
        }
        
        [ContextMenu("Compute Chance")]
        private void ComputeChance()
        {
            var curChance = 0f;
            var totalWeight = 0f;
            for (int i = 0; i < Weights.Length; i++)
            {
                totalWeight += Weights[i].Weight;
            }
            
            
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i].SetLowerChance(curChance);
                curChance += Weights[i].Weight/totalWeight;
                Weights[i].SetUpperChance(curChance);
            }
        }
    }
}

