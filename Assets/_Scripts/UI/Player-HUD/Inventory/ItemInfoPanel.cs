using System.Collections;
using SGGames.Scripts.Data;
using SGGames.Scripts.EditorExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class ItemInfoPanel : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private ItemData m_data;
        [SerializeField] private ConstantData m_constantData;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_bg;
        [SerializeField] private Sprite m_commonBGSprite;
        [SerializeField] private Sprite m_uncommonBGSprite;
        [SerializeField] private Sprite m_rareBGSprite;
        [SerializeField] private Sprite m_epicBGSprite;
        [SerializeField] private Sprite m_legendaryBGSprite;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_rarityType;
        [SerializeField] private TextMeshProUGUI m_categoryType;
        [SerializeField] private ModifierUIGroupPanel m_modifierUIGroup;
        
        private bool m_coroutineRunning;
        
        private void Start()
        {
            Hide();
        }
        
        private void ResetView()
        {
            m_data = null;
            m_modifierUIGroup.ResetView();
        }

        public void Show(ItemData data)
        {
            if (m_coroutineRunning) return;
            StartCoroutine(OnShowing(data));
        }

        private IEnumerator OnShowing(ItemData data)
        {
            m_coroutineRunning = true;
            if (data != m_data)
            {
                ResetView();
                m_data = data;
                FillInfo(data);
            }
            yield return new WaitForEndOfFrame();
            m_canvasGroup.alpha = 1;
            m_coroutineRunning = false;
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
                case Rarity.Epic:
                    m_bg.sprite = m_epicBGSprite;
                    break;
                case Rarity.Legendary:
                    m_bg.sprite = m_legendaryBGSprite;
                    break;
            }
            
            m_name.color = m_constantData.GetRarityColor(data.Rarity);
            m_name.text = data.TranslatedName;
            
            m_rarityType.color = m_constantData.GetRarityColor(data.Rarity);
            m_rarityType.text = data.Rarity.ToString();

            switch (data.ItemCategory)
            {
                case ItemCategory.Helmet:
                    m_modifierUIGroup.Show((HelmetData)data);
                    break;
                case ItemCategory.Armor:
                    m_modifierUIGroup.Show((ArmorData)data);
                    break;
                case ItemCategory.Boots:
                    m_modifierUIGroup.Show((BootsData)data);
                    break;
                case ItemCategory.Gloves:
                    m_modifierUIGroup.Show((GlovesData)data);
                    break;
                case ItemCategory.Accessories:
                    m_modifierUIGroup.Show((AccessoriesData)data);
                    break;
                case ItemCategory.Charm:
                    m_modifierUIGroup.Show((CharmData)data);
                    break;
            }
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}

