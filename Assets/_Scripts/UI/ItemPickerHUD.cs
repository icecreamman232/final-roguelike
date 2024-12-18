using System;
using SGGames.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class ItemPickerHUD : MonoBehaviour
    {
        [SerializeField] private ConstantData m_constantData;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_bg;
        [SerializeField] private Sprite m_commonBGSprite;
        [SerializeField] private Sprite m_uncommonBGSprite;
        [SerializeField] private Sprite m_rareBGSprite;
        [SerializeField] private Sprite m_legendaryBGSprite;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_rarityType;
        [SerializeField] private TextMeshProUGUI m_categoryType;
        
        private bool m_hasBeenDisplayed;

        private void Start()
        {
            Hide();
        }

        public void Show(ItemData data)
        {
            m_canvasGroup.alpha = 1;
            if (!m_hasBeenDisplayed)
            {
                FillInfo(data);
                m_hasBeenDisplayed = true;
            }
        }

        protected virtual void FillInfo(ItemData data)
        {
            switch (data.Rarity)
            {
                case Rarity.Common:
                    m_bg.sprite = m_commonBGSprite;
                    break;
                case Rarity.Uncommon:
                    m_bg.sprite = m_uncommonBGSprite;
                    break;
                case Rarity.Rare:
                    m_bg.sprite = m_rareBGSprite;
                    break;
                case Rarity.Legendary:
                    m_bg.sprite = m_legendaryBGSprite;
                    break;
            }
            
            m_name.color = m_constantData.GetRarityColor(data.Rarity);
            m_name.text = data.TranslatedName;
            
            m_rarityType.color = m_constantData.GetRarityColor(data.Rarity);
            m_rarityType.text = data.Rarity.ToString();
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
