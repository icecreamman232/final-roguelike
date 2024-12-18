using System;
using SGGames.Scripts.Common;
using SGGames.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class UpgradeAttributeCardUI : Button
    {
        [SerializeField] private int m_index;
        [SerializeField] private Image m_bg;
        [SerializeField] private Sprite m_commonBG;
        [SerializeField] private Sprite m_uncommonBG;
        [SerializeField] private Sprite m_rareBG;
        [SerializeField] private Sprite m_legendaryBG;
        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private TextMeshProUGUI m_descText;

        private (UpgradeAttributeRate rate, AttributeType type) m_reward;
        
        public (UpgradeAttributeRate rate, AttributeType type) Reward => m_reward;
        
        public Action<int> OnSelectCard;
        
        public void Show((UpgradeAttributeRate rate, AttributeType type) attributeReward)
        {
            m_reward = attributeReward;
            switch (attributeReward.rate)
            {
                case UpgradeAttributeRate.Common:
                    m_bg.sprite = m_commonBG;
                    break;
                case UpgradeAttributeRate.Uncommon:
                    m_bg.sprite = m_uncommonBG;
                    break;
                case UpgradeAttributeRate.Rare:
                    m_bg.sprite = m_rareBG;
                    break;
                case UpgradeAttributeRate.Legendary:
                    m_bg.sprite = m_legendaryBG;
                    break;
            }

            m_titleText.text = GetTitleText(attributeReward.type) + " Upgrade";
            m_descText.text = GetDescText(attributeReward.type, attributeReward.rate);
        }

        private string GetTitleText(AttributeType type)
        {
            switch (type)
            {
                case AttributeType.Strength:
                    return "Strength";
                case AttributeType.Agility:
                    return "Agility";
                case AttributeType.Intelligence:
                    return "Intelligence";
            }
            return "Strength";
        }

        private string GetDescText(AttributeType type , UpgradeAttributeRate rate)
        {
            //f4b41b is yellow
            switch (rate)
            {
                case UpgradeAttributeRate.Common:
                    return $"<color=#f4b41b>+{AttributeRewardController.CommonRewardPoint}</color> {GetTitleText(type)}";
                case UpgradeAttributeRate.Uncommon:
                    return $"<color=#f4b41b>+{AttributeRewardController.UncommonRewardPoint}</color> {GetTitleText(type)}";
                case UpgradeAttributeRate.Rare:
                    return $"<color=#f4b41b>+{AttributeRewardController.RareRewardPoint}</color> {GetTitleText(type)}";
                case UpgradeAttributeRate.Legendary:
                    return $"<color=#f4b41b>+{AttributeRewardController.LegendaryRewardPoint}</color> {GetTitleText(type)}";
            }
            return $"+ {AttributeRewardController.CommonRewardPoint} {GetTitleText(type)}";
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnSelectCard.Invoke(m_index);
        }
    }
}
