using System;
using MoreMountains.Feedbacks;
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
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private MMF_Player m_showFeedback;
        [SerializeField] private MMF_Player m_notSelectFeedback;

        private (AttributeTier rate, AttributeType type) m_reward;
        
        public (AttributeTier rate, AttributeType type) Reward => m_reward;
        
        public Action<int> OnSelectCard;

        private Vector2 m_initAnchorMin;
        private Vector2 m_initAnchorMax;

        protected override void Start()
        {
            base.Start();
            m_initAnchorMin = ((RectTransform)transform).anchorMin;
            m_initAnchorMax = ((RectTransform)transform).anchorMax;
        }

        public void Show((AttributeTier rate, AttributeType type) attributeReward)
        {
            //Reset values
            ((RectTransform)transform).anchorMin = m_initAnchorMin;
            ((RectTransform)transform).anchorMax = m_initAnchorMax;
            m_canvasGroup.alpha = 1;
            
            m_showFeedback.PlayFeedbacks();
            m_reward = attributeReward;
            switch (attributeReward.rate)
            {
                case AttributeTier.Tier1:
                    m_bg.sprite = m_commonBG;
                    break;
                case AttributeTier.Tier2:
                    m_bg.sprite = m_uncommonBG;
                    break;
                case AttributeTier.Tier3:
                    m_bg.sprite = m_rareBG;
                    break;
                case AttributeTier.Tier4:
                    m_bg.sprite = m_legendaryBG;
                    break;
            }

            m_titleText.text = GetTitleText(attributeReward.type) + " Upgrade";
            m_descText.text = GetDescText(attributeReward.type, attributeReward.rate);
        }

        public void PlayNotSelectFeedback()
        {
            m_notSelectFeedback.PlayFeedbacks();
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

        private string GetDescText(AttributeType type , AttributeTier rate)
        {
            //f4b41b is yellow
            switch (rate)
            {
                case AttributeTier.Tier1:
                    return $"<color=#f4b41b>+{AttributeRewardController.CommonRewardPoint}</color> {GetTitleText(type)}";
                case AttributeTier.Tier2:
                    return $"<color=#f4b41b>+{AttributeRewardController.UncommonRewardPoint}</color> {GetTitleText(type)}";
                case AttributeTier.Tier3:
                    return $"<color=#f4b41b>+{AttributeRewardController.RareRewardPoint}</color> {GetTitleText(type)}";
                case AttributeTier.Tier4:
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
