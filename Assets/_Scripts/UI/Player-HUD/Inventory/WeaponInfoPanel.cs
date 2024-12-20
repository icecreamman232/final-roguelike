using System.Collections;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class WeaponInfoPanel : MonoBehaviour
    {
        [SerializeField][ReadOnly] private WeaponData m_data;
        [SerializeField] private ConstantData m_constantData;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private Image m_bg;
        [SerializeField] private Sprite m_commonBG;
        [SerializeField] private Sprite m_uncommonBG;
        [SerializeField] private Sprite m_rareBG;
        [SerializeField] private Sprite m_legendaryBG;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_rarityType;
        [SerializeField] private TextMeshProUGUI m_categoryType;
        [SerializeField] private TextMeshProUGUI m_rangeValue;
        [SerializeField] private TextMeshProUGUI m_damageValue;
        [SerializeField] private TextMeshProUGUI m_atkSpdValue;
        [SerializeField] private ModifierUIGroupPanel m_modifierGroupPanel;

        private bool m_isCoroutineRunning;
        
        private void Start()
        {
            Hide();
        }

        public void Show(WeaponData data)
        {
            if (m_isCoroutineRunning) return;
            StartCoroutine(OnShowing(data));
        }

        private IEnumerator OnShowing(WeaponData data)
        {
            m_isCoroutineRunning = true;
            if (data != m_data)
            {
                ResetView();
                FillInfo(data);
                m_data = data;
            }
            yield return new WaitForEndOfFrame();
            m_canvasGroup.alpha = 1;
            m_isCoroutineRunning = false;
        }

        private void ResetView()
        {
            m_data = null;
            m_modifierGroupPanel.ResetView();
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

            m_modifierGroupPanel.Show(data);
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}

