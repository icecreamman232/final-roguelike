using SGGames.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class WeaponPickerHUD : MonoBehaviour
    {
        [SerializeField] private ConstantData m_constantData;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_bg;
        [SerializeField] private Sprite m_commonBG;
        [SerializeField] private Sprite m_uncommonBG;
        [SerializeField] private Sprite m_rareBG;
        [SerializeField] private Sprite m_epicBG;
        [SerializeField] private Sprite m_legendaryBG;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_rarityType;
        [SerializeField] private TextMeshProUGUI m_categoryType;
        [SerializeField] private TextMeshProUGUI m_rangeValue;
        [SerializeField] private TextMeshProUGUI m_damageValue;
        [SerializeField] private TextMeshProUGUI m_atkSpdValue;

        private bool m_hasBeenDisplayed;

        private void Start()
        {
            Hide();
        }

        public void Show(WeaponData data)
        {
            m_canvasGroup.alpha = 1;
            if (!m_hasBeenDisplayed)
            {
                FillInfo(data);
                m_hasBeenDisplayed = true;
            }
        }

        private void FillInfo(WeaponData data)
        {
            switch (data.Rarity)
            {
                case Rarity.Common:
                    m_bg.sprite = m_commonBG;
                    break;
                case Rarity.Uncommon:
                    m_bg.sprite = m_uncommonBG;
                    break;
                case Rarity.Rare:
                    m_bg.sprite = m_rareBG;
                    break;
                case Rarity.Epic:
                    m_bg.sprite = m_epicBG;
                    break;
                case Rarity.Legendary:
                    m_bg.sprite = m_legendaryBG;
                    break;
            }
            
            m_name.color = m_constantData.GetRarityColor(data.Rarity);
            m_name.text = data.TranslatedName;
            
            m_rarityType.color = m_constantData.GetRarityColor(data.Rarity);
            m_rarityType.text = data.Rarity.ToString();
            
            m_categoryType.text = data.WeaponCategory.ToString();
            m_rangeValue.text = data.AttackRange.ToString("F1");
            m_damageValue.text = $"{data.MinDamage} - {data.MaxDamage}";
            m_atkSpdValue.text = (1f / data.BaseDelayBetweenShots).ToString("F1");
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
