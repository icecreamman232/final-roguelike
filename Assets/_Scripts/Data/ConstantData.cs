using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "ConstantData", menuName = "SGGames/Data/Constants")]
    [PreferBinarySerialization]
    public class ConstantData : ScriptableObject
    {
        [Header("Color")] 
        [SerializeField] private Color m_rarityCommonColor;
        [SerializeField] private Color m_rarityUncommonColor;
        [SerializeField] private Color m_rarityRareColor;
        [SerializeField] private Color m_rarityLegendaryColor;
        [Header("Constants")]
        [SerializeField] private float m_maxAtkSpd;
        [SerializeField] private float m_strToRegenerate;
        [SerializeField] private float m_strToHealth;
        [SerializeField] private float m_agiToAtkSpd;
        [SerializeField] private float m_atkSpdToAtkRate;


        public void SetData(float maxAtkSpd, float strToRegenerate, 
            float strToHealth, float agiToAtkSpd, float atkSpdToAtkRate)
        {
            m_maxAtkSpd = maxAtkSpd;
            m_strToRegenerate = strToRegenerate;
            m_strToHealth = strToHealth;
            m_agiToAtkSpd = agiToAtkSpd;
            m_atkSpdToAtkRate = atkSpdToAtkRate;
        }

        public Color GetRarityColor(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return m_rarityCommonColor;
                case Rarity.Uncommon:
                    return m_rarityUncommonColor;
                case Rarity.Rare:
                    return m_rarityRareColor;
                case Rarity.Legendary:
                    return m_rarityLegendaryColor;
            }
            return Color.white;
        }
        
        public Color RarityCommonColor => m_rarityCommonColor;
        public Color RarityUncommonColor => m_rarityUncommonColor;
        public Color RarityRareColor => m_rarityRareColor;
        public Color RarityLegendaryColor => m_rarityLegendaryColor;
        
        public float C_MAX_ATK_SPD => m_maxAtkSpd;
        public float C_STR_TO_REGENERATE => m_strToRegenerate;
        public float C_STR_TO_HEALTH => m_strToHealth;
        public float C_AGI_TO_ATK_SPD => m_agiToAtkSpd;
        public float C_ATK_SPD_TO_ATK_RATE => m_atkSpdToAtkRate;
        
    }
}
